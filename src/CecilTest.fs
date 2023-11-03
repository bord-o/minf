module CecilTest

open System
open Mono.Cecil
open Mono.Cecil.Cil

let make_app () = 
    let myHelloWorldApp = 
        AssemblyDefinition.CreateAssembly(
            new AssemblyNameDefinition("HelloWorld", new Version(1, 0, 0, 0)), "HelloWorld", ModuleKind.Console)

    let module_ = myHelloWorldApp.MainModule

    // create the program type and add it to the module
    let programType = 
        new TypeDefinition("HelloWorld", "Program",
            Mono.Cecil.TypeAttributes.Class ||| Mono.Cecil.TypeAttributes.Public, module_.TypeSystem.Object)

    module_.Types.Add(programType)

    // add an empty constructor
    let ctor = 
        new MethodDefinition(".ctor", Mono.Cecil.MethodAttributes.Public ||| Mono.Cecil.MethodAttributes.HideBySig
            ||| Mono.Cecil.MethodAttributes.SpecialName ||| Mono.Cecil.MethodAttributes.RTSpecialName, module_.TypeSystem.Void)

    // create the constructor's method body
    let il = ctor.Body.GetILProcessor()

    il.Append(il.Create(OpCodes.Ldarg_0))

    // call the base constructor
    il.Append(il.Create(OpCodes.Call, module_.ImportReference(typeof<obj>.GetConstructor([||]))))

    il.Append(il.Create(OpCodes.Nop))
    il.Append(il.Create(OpCodes.Ret))

    programType.Methods.Add(ctor)

    // define the 'Main' method and add it to 'Program'
    let mainMethod = 
        new MethodDefinition("Main",
            Mono.Cecil.MethodAttributes.Public ||| Mono.Cecil.MethodAttributes.Static, module_.TypeSystem.Void)

    programType.Methods.Add(mainMethod)

    // add the 'args' parameter
    let argsParameter = 
        new ParameterDefinition("args",
            Mono.Cecil.ParameterAttributes.None, module_.ImportReference(typeof<string[]>))

    mainMethod.Parameters.Add(argsParameter);

    // create the method body
    let ilm = mainMethod.Body.GetILProcessor()

    ilm.Append(il.Create(OpCodes.Nop))
    ilm.Append(il.Create(OpCodes.Ldstr, "Hello World"))

    let writeLineMethod = 
        ilm.Create(OpCodes.Call,
            module_.ImportReference(typeof<Console>.GetMethod("WriteLine", [|typeof<string>|])))

    // call the method
    ilm.Append(writeLineMethod)

    ilm.Append(il.Create(OpCodes.Nop))
    ilm.Append(il.Create(OpCodes.Ret))

    // set the entry point and save the module
    printfn "creating main method"
    myHelloWorldApp.EntryPoint <- mainMethod
    printfn "Saving..."
    myHelloWorldApp.Write("Test.exe")

