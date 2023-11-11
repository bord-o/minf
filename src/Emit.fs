module Emit
open System
open System.Reflection
open System.Reflection.Emit

let assemblyName = new AssemblyName("DynamicAssemblyExample")
let assemblyBuilder =
    AssemblyBuilder.DefineDynamicAssembly(
        assemblyName,
        AssemblyBuilderAccess.Run)

// The module name is usually the same as the assembly name.
let moduleBuilder =
    assemblyBuilder.DefineDynamicModule(assemblyName.Name)
let typeBuilder =
    moduleBuilder.DefineType(
        "MyDynamicType",
        TypeAttributes.Public)

// Define a constructor1 that takes an integer argument and
// stores it in the private field.
let parameterTypes = [| |]
let ctor1 =
    typeBuilder.DefineConstructor(
        MethodAttributes.Public,
        CallingConventions.Standard,
        parameterTypes)

let ctor1IL = ctor1.GetILGenerator()

// For a constructor, argument zero is a reference to the new
// instance. Push it on the stack before calling the base
// class constructor. Specify the default constructor of the
// base class (System.Object) by passing an empty array of
// types (Type.EmptyTypes) to GetConstructor.
ctor1IL.Emit(OpCodes.Ldarg_0)
ctor1IL.Emit(OpCodes.Call,
                 typeof<obj>.GetConstructor(Type.EmptyTypes))
ctor1IL.Emit(OpCodes.Ret)



// Define a method that accepts an integer argument and returns
// the product of that integer and the private field m_number. This
// time, the array of parameter types is created on the fly.
let methodBuilder =
    typeBuilder.DefineMethod(
        "Main",
        MethodAttributes.Public,
        typeof<int>,
        [| |])

let methodIL = methodBuilder.GetILGenerator()
// To retrieve the private instance field, load the instance it
// belongs to (argument zero). After loading the field, load the
// argument one and then multiply. Return from the method with
// the return value (the product of the two numbers) on the
// execution stack.
methodIL.Emit(OpCodes.Ldc_I4, 99)
methodIL.Emit(OpCodes.Ret)

// Finish the type
let typ = typeBuilder.CreateType()
let asm = Assembly.GetAssembly(typ)

let createDll () = 
    let generator = new Lokad.ILPack.AssemblyGenerator()
    printfn "Making dll"
    generator.GenerateAssembly(asm, "/home/bordo/minf/gen/Gen.dll")
    printfn "done"
