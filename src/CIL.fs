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


let main_il = ()

//--------------------------------------------------------------------------------------------------------------------
// CIL Assembly generation testing
let make_test_app () =
    //make an assembly
    let appAsm =
        AssemblyDefinition.CreateAssembly(
            new AssemblyNameDefinition("CodeGen", new Version(1, 0, 0, 0)),
            "CodeGen",
            ModuleKind.Console
        )
    // get the module from the assembly
    let module_ = appAsm.MainModule

    // create the program type(class) and add it to the module
    let programType =
        new TypeDefinition(
            "CodeGen",
            "Program",
            TypeAttributes.Class ||| TypeAttributes.Public,
            module_.TypeSystem.Object
        )
    module_.Types.Add(programType)
    let get_func n = func n programType

    // add an empty constructor for the program type
    let ctor =
        new MethodDefinition(
            ".ctor",
            MethodAttributes.Public
            ||| MethodAttributes.HideBySig
            ||| MethodAttributes.SpecialName
            ||| MethodAttributes.RTSpecialName,
            module_.TypeSystem.Void
        )

    // create the constructor's method body
    let il = ctor.Body.GetILProcessor()
    il.Emit(OpCodes.Ldarg_0)
    il.Emit(OpCodes.Call, module_.ImportReference(typeof<obj>.GetConstructor([||])))
    il.Emit(OpCodes.Nop)
    il.Emit(OpCodes.Ret)
    programType.Methods.Add(ctor)


    //-------------------------------------------------------------------------
    // define an add9 method and add it to 'Program'
    let add9Method =
        makeFunc "add9" module_.TypeSystem.Int32 module_.TypeSystem.Int32 programType

    let add9 = add9Method.Body.GetILProcessor()

    add9.Emit(OpCodes.Ldc_I4, 9)
    add9.Emit(OpCodes.Ldarg_0)
    add9.Emit(OpCodes.Add)
    add9.Emit(OpCodes.Ret)

    //-------------------------------------------------------------------------
    // define the 'Main' method and add it to 'Program'
    let mainMethod =
        makeFunc "Main" (module_.ImportReference typeof<string[]>) module_.TypeSystem.Int32 programType

    // start defining the method body
    let main = mainMethod.Body.GetILProcessor()
    main.Emit(OpCodes.Nop)
    main.Emit(OpCodes.Ldstr, "generic method creation woohoo")
    // call the method
    main.Emit(OpCodes.Call, module_.ImportReference(typeof<Console>.GetMethod("WriteLine", [| typeof<string> |])))
    main.Emit(OpCodes.Ldc_I4, 10)
    main.Emit(OpCodes.Call, module_.ImportReference(get_func "add9"))
    endIl main

    // set the entry point and save the module
    printfn "creating main method"
    appAsm.EntryPoint <- mainMethod
    printfn "Saving..."
    appAsm.Write("Main.dll")
