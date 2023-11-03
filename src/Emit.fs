module Emit

open System.Reflection
open System.Reflection.Emit


let aName = new AssemblyName("CIL Example")
let  ab: AssemblyBuilder =
    AssemblyBuilder.DefineDynamicAssembly(
        aName,
        AssemblyBuilderAccess.Run)

// The module name is usually the same as the assembly name.
//let mb = ab.DefineDynamicModule(aName.Name);

//let tb = mb.DefineType("Main", TypeAttributes.Public)

//let method1 = tb.DefineMethod("testadd", MethodAttributes.Public,CallingConventions.Standard, typeof<int>, [|typeof<int>|] )

//let method1IL = method1.GetILGenerator();
//do 
//   method1IL.Emit(OpCodes.Ldarg_0) 
//   method1IL.Emit(OpCodes.Ldind_I8, 99)
//   method1IL.Emit(OpCodes.Add)
//   method1IL.Emit(OpCodes.Ret) 
//let t = tb.CreateType()

//ignore <| ab.Save("./CIL.test")



