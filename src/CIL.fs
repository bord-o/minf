module CIL

open System
open System.Linq
open Mono.Cecil
open Mono.Cecil.Cil

//-------------------------------------------------------------------------
// CIL generation helpers
/// <summary>This function makes a static method with one argument with the corresponding types</summary>
/// <returns>The IL generator for the created function</returns>
let makeFunc name (intype: TypeReference) (outtype: TypeReference) (main: TypeDefinition) =
    let meth =
        new MethodDefinition(name, Mono.Cecil.MethodAttributes.Public ||| Mono.Cecil.MethodAttributes.Static, outtype)

    let param =
        new ParameterDefinition("arg", Mono.Cecil.ParameterAttributes.None, intype)

    ignore <| meth.Parameters.Add(param)
    ignore <| main.Methods.Add(meth)
    meth

// need to add locals at the top of the method
// write a function to go through the AST and grab all of the var declarations and their types
let makeVar name (type': TypeReference) (main: TypeDefinition) (locals) =
    let loc = new VariableDefinition(type')
    loc :: locals

let endIl (func: ILProcessor) =
    func.Emit(OpCodes.Nop)
    func.Emit(OpCodes.Ret)

//--------------------------------------------------------------------------------------------------------------------
// CIL env helpers
let func n (program: TypeDefinition) = 
    program.Methods.Where(fun m -> m.Name = n).First()

let initEnv () = ()
let completeEnv () = ()
let saveAssembly () = ()

//--------------------------------------------------------------------------------------------------------------------
// CIL Assembly generation testing
//make an assembly
let appAsm =
    AssemblyDefinition.CreateAssembly(
        new AssemblyNameDefinition("CodeGen", new Version(1, 0, 0, 0)),
        "CodeGen",
        ModuleKind.Console
    )
// get the module from the assembly
let appModule = appAsm.MainModule

// create the program type(class) and add it to the module
let programType =
    new TypeDefinition(
        "CodeGen",
        "Program",
        TypeAttributes.Class ||| TypeAttributes.Public,
        appModule.TypeSystem.Object
    )
appModule.Types.Add(programType)
let get_func n = func n programType

// add an empty constructor for the program type
let ProgramCtor =
    new MethodDefinition(
        ".ctor",
        MethodAttributes.Public
        ||| MethodAttributes.HideBySig
        ||| MethodAttributes.SpecialName
        ||| MethodAttributes.RTSpecialName,
        appModule.TypeSystem.Void
    )

// create the constructor's method body
let ProgramCtorIl = ProgramCtor.Body.GetILProcessor()
ProgramCtorIl.Emit(OpCodes.Ldarg_0)
ProgramCtorIl.Emit(OpCodes.Call, appModule.ImportReference(typeof<obj>.GetConstructor([||])))
ProgramCtorIl.Emit(OpCodes.Nop)
ProgramCtorIl.Emit(OpCodes.Ret)
programType.Methods.Add(ProgramCtor)


//-------------------------------------------------------------------------
// define an add9 method and add it to 'Program'
let add9Method =
    makeFunc "add9" appModule.TypeSystem.Int32 appModule.TypeSystem.Int32 programType

let add9Il = add9Method.Body.GetILProcessor()

add9Il.Emit(OpCodes.Ldc_I4, 9)
add9Il.Emit(OpCodes.Ldarg_0)
add9Il.Emit(OpCodes.Add)
add9Il.Emit(OpCodes.Ret)

//-------------------------------------------------------------------------
// define the 'Main' method and add it to 'Program'
let mainMethod =
    makeFunc "Main" (appModule.ImportReference typeof<string[]>) appModule.TypeSystem.Int32 programType

// start defining the method body
let mainIl = mainMethod.Body.GetILProcessor()
mainIl.Emit(OpCodes.Nop)
mainIl.Emit(OpCodes.Ldstr, "generic method creation woohoo")
// call the method
mainIl.Emit(OpCodes.Call, appModule.ImportReference(typeof<Console>.GetMethod("WriteLine", [| typeof<string> |])))
mainIl.Emit(OpCodes.Ldc_I4, 10)
mainIl.Emit(OpCodes.Call, appModule.ImportReference(get_func "add9"))
endIl mainIl

// set the entry point and save the module
printfn "creating main method"
appAsm.EntryPoint <- mainMethod

let make_test_app () =
    printfn "Saving..."
    appAsm.Write("Main.dll")
