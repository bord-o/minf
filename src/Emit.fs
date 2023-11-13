module Emit
open System
open System.Reflection
open System.Reflection.Emit

module Locals =
    type t = (string * LocalBuilder) list
    let empty: t = []

    let get table search =
        List.find (fun (k, v) -> k = search) table |> snd

    let enter k v table = (k, v) :: table


type env = 
    {
        prog: TypeBuilder //use this for adding methods
        main: ILGenerator //use this for adding let bindings and entrypoint
        mutable locals: Locals.t
    } 

let initAsm () =
    let assemblyName = new AssemblyName("Asm")
    let assemblyBuilder =
        AssemblyBuilder.DefineDynamicAssembly(
            assemblyName,
            AssemblyBuilderAccess.Run)

    // The module name is usually the same as the assembly name.
    let moduleBuilder =
        assemblyBuilder.DefineDynamicModule(assemblyName.Name)

    // define program class
    let programTyBldr =
        moduleBuilder.DefineType(
            "Program",
            TypeAttributes.Public)

    //  Program ctor (does nothing) 
    let parameterTypes = [| |]
    let programCtor =
        programTyBldr.DefineConstructor(
            MethodAttributes.Public,
            CallingConventions.Standard,
            parameterTypes)
    let ctor1IL = programCtor.GetILGenerator()
    ctor1IL.Emit(OpCodes.Ldarg_0)
    ctor1IL.Emit(OpCodes.Call,
                     typeof<obj>.GetConstructor(Type.EmptyTypes))
    ctor1IL.Emit(OpCodes.Ret)

    // define our main function that acts as entrypoint
    let mainBldr =
        programTyBldr.DefineMethod(
            "Main",
            MethodAttributes.Public,
            typeof<int>,
            [| |])
    let mainIL = mainBldr.GetILGenerator()
    mainIL.Emit(OpCodes.Ldstr, "Entering Main in generated code...")
    mainIL.Emit(OpCodes.Call, typeof<Console>.GetMethod("WriteLine", [|typeof<string>|]))
    //mainIL.Emit(OpCodes.Ldc_I4, 26)
    {prog=programTyBldr; main=mainIL; locals=Locals.empty}


let finishAsm (env:env) = 
    env.main.Emit(OpCodes.Ret) //finish main by returning
    // Finish the program type
    let typ = env.prog.CreateType() // finish program type 
    Assembly.GetAssembly(typ)

let createDll asm path = 
    let generator = new Lokad.ILPack.AssemblyGenerator()
    printfn "Making dll"
    generator.GenerateAssembly(asm, path)
    printfn "done"
