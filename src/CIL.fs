module CIL
open System
open System.Linq
open Mono.Cecil
open Mono.Cecil.Cil



/// <summary>This function makes a static method with one argument with the corresponding types</summary>
/// <returns>The IL generator for the created function</returns>
let makeFunc name (intype: TypeReference) (outtype: TypeReference) (main: TypeDefinition) = 
    let meth = 
        new MethodDefinition(name,
            Mono.Cecil.MethodAttributes.Public ||| Mono.Cecil.MethodAttributes.Static, outtype)
    let param = 
        new ParameterDefinition("arg",
            Mono.Cecil.ParameterAttributes.None, intype)
    ignore <| meth.Parameters.Add(param)
    ignore <| main.Methods.Add(meth)
    meth

let endIl (func: ILProcessor) = 
    func.Append(func.Create(OpCodes.Nop))
    func.Append(func.Create(OpCodes.Ret))


let make_app () = 
    let appAsm = 
        AssemblyDefinition.CreateAssembly(
            new AssemblyNameDefinition("CodeGen", new Version(1, 0, 0, 0)), "CodeGen", ModuleKind.Console)

    let module_ = appAsm.MainModule

    // create the program type and add it to the module
    let programType = 
        new TypeDefinition("CodeGen", "Program",
            Mono.Cecil.TypeAttributes.Class ||| Mono.Cecil.TypeAttributes.Public, module_.TypeSystem.Object)

    module_.Types.Add(programType)

    // add an empty constructor
    let ctor = 
        new MethodDefinition(".ctor", Mono.Cecil.MethodAttributes.Public ||| Mono.Cecil.MethodAttributes.HideBySig
            ||| Mono.Cecil.MethodAttributes.SpecialName ||| Mono.Cecil.MethodAttributes.RTSpecialName, module_.TypeSystem.Void)

    // create the constructor's method body
    let il = ctor.Body.GetILProcessor()
    il.Append(il.Create(OpCodes.Ldarg_0))
    il.Append(il.Create(OpCodes.Call, module_.ImportReference(typeof<obj>.GetConstructor([||]))))
    il.Append(il.Create(OpCodes.Nop))
    il.Append(il.Create(OpCodes.Ret))
    programType.Methods.Add(ctor)


    //--------------------------------------------------------------------------------------------------------------------
    // define an add9 method and add it to 'Program'
    let add9Method = makeFunc "add9" module_.TypeSystem.Int32 module_.TypeSystem.Int32 programType
    let add9 = add9Method.Body.GetILProcessor()
    
    add9.Append(add9.Create(OpCodes.Ldc_I4, 9))
    add9.Append(add9.Create(OpCodes.Ldarg_0))
    add9.Append(add9.Create(OpCodes.Add))
    add9.Append(add9.Create(OpCodes.Ret))

    //--------------------------------------------------------------------------------------------------------------------
    // define the 'Main' method and add it to 'Program'
    let mainMethod = makeFunc "Main" (module_.ImportReference typeof<string[]>) module_.TypeSystem.Int32 programType
    let main = mainMethod.Body.GetILProcessor()

    // create the method body
    main.Append(main.Create(OpCodes.Nop))
    main.Append(main.Create(OpCodes.Ldstr, "generic method creation woohoo"))
    // call the method
    main.Append <|
        main.Create(OpCodes.Call,
            module_.ImportReference(typeof<Console>.GetMethod("WriteLine", [|typeof<string>|])))

    main.Append(main.Create(OpCodes.Ldc_I4, 10))

    main.Append <|
        main.Create(OpCodes.Call, 
            module_.ImportReference(programType.Methods.Where(fun m -> m.Name = "add9").First()))
    // main.Append <|
    //     main.Create(OpCodes.Call,
    //         module_.ImportReference(typeof<Console>.GetMethod("WriteLine", [|typeof<int>|])))
    //
    endIl main



    // set the entry point and save the module
    printfn "creating main method"
    appAsm.EntryPoint <- mainMethod
    printfn "Saving..."
    appAsm.Write("Main.dll")

