module CodeGen

open System.Reflection.Emit
open System.Reflection

module A = Ast
module T = Type
module E = Emit

// This module will call out to the CIL module for all il generation functions


let rec eval_exp node (env: E.env) =
    // this function needs access to the the ilGenerator for the containing method
    let eval_exp' e = eval_exp e env

    match node with
    | A.OpExp(e1, op, e2) ->
        match op with
        // emit ldc_I4 (eval_exp' env e1)
        // emit ldc_I4 (eval_exp' env e2)
        // emit add
        | A.Plus ->
            eval_exp' e1
            eval_exp' e2
            env.main.Emit(OpCodes.Add)
            printfn "add"
        | A.Minus ->
            eval_exp' e1
            eval_exp' e2
            env.main.Emit(OpCodes.Sub)
            printfn "sub"
        | A.Times ->
            eval_exp' e1
            eval_exp' e2
            env.main.Emit(OpCodes.Mul)
            printfn "mul"

        | A.Neq ->
            eval_exp' e1
            eval_exp' e2
            env.main.Emit(OpCodes.Ceq)
            printfn "ceq" // 1 if equal
            env.main.Emit(OpCodes.Ldc_I4_0)
            printfn "ldc_I4 0"
            env.main.Emit(OpCodes.Ceq)
            printfn "ceq"
        | A.Eq ->
            eval_exp' e1
            eval_exp' e2
            env.main.Emit(OpCodes.Ceq)
            printfn "ceq" // 1 if equal
        | A.LT ->
            eval_exp' e1
            eval_exp' e2
            printfn "clt"
            env.main.Emit(OpCodes.Clt)
        | A.LTE ->
            let label = env.main.DefineLabel()
            let l0 = env.main.DeclareLocal(typeof<int>)
            env.main.Emit(OpCodes.Ldc_I4, 1)
            env.main.Emit(OpCodes.Stloc, l0)
            eval_exp' e1
            eval_exp' e2
            env.main.Emit(OpCodes.Ble, label)
            env.main.Emit(OpCodes.Ldc_I4, 0)
            env.main.Emit(OpCodes.Stloc, l0)
            env.main.MarkLabel(label)
            env.main.Emit(OpCodes.Ldloc, l0)

        | A.GT ->
            eval_exp' e1
            eval_exp' e2
            printfn "cgt"
            env.main.Emit(OpCodes.Cgt)
        | A.GTE ->
            let label = env.main.DefineLabel()
            let l0 = env.main.DeclareLocal(typeof<int>)
            env.main.Emit(OpCodes.Ldc_I4, 1)
            env.main.Emit(OpCodes.Stloc, l0)
            eval_exp' e1
            eval_exp' e2
            env.main.Emit(OpCodes.Bge, label)
            env.main.Emit(OpCodes.Ldc_I4, 0)
            env.main.Emit(OpCodes.Stloc, l0)
            env.main.MarkLabel(label)
            env.main.Emit(OpCodes.Ldloc, l0)

    | A.NumExp(i) ->
        env.main.Emit(OpCodes.Ldc_I4, i)
        printfn $"ldc_I4 {i}"
    | A.BoolExp(b) ->
        env.main.Emit(OpCodes.Ldc_I4, (if b then 1 else 0))
        printfn $"ldc_I4 {if b then 1 else 0}"

    | A.IfExp(if', then', else') ->
        // to simply compile a conditional, we create a label(instruction) for the else branch
        // and the end. The end instruction doesn't return, it just leaves the result of the expression
        // at the top of the stack
        // we can use a small assoc list for the labeled opcodes
        let else_l = env.main.DefineLabel()
        let end_l = env.main.DefineLabel()
        let res = env.main.DeclareLocal(typeof<int>)
        printfn $"making label else: {eval_exp else'}" // let lables = ("else", il.Create(Nop))::labels
        printfn $"making label end: ldloc 1" // let lables = ("end", il.Create(Ldloc, 1))::labels

        eval_exp' if'
        env.main.Emit(OpCodes.Brfalse, else_l)
        printfn $"brfalse  else" //bool

        eval_exp' then'
        env.main.Emit(OpCodes.Stloc, res)
        env.main.Emit(OpCodes.Br, end_l)

        env.main.MarkLabel(else_l)
        eval_exp' else'
        env.main.Emit(OpCodes.Stloc, res)
        env.main.Emit(OpCodes.Br, end_l)

        env.main.MarkLabel(end_l)
        env.main.Emit(OpCodes.Ldloc, res)

    | A.CallExp(funname, arg) ->
        let name =
            match funname with
            | A.Fun x -> x
            | _ -> failwith "expecting a fun name"

        printfn $"calling {name}"
        let method = env.prog.GetMethod(name)
        // let methodArg = method.GetParameters()[0]
        // let argName = methodArg.Name
        // printfn "%A" argName
        // let argVal = env.main.DeclareLocal(typeof<int>)
        // env.locals <- E.Locals.enter argName argVal env.locals // our single mutation to make the arg in scope of the function
        eval_exp' arg 

        env.main.Emit(OpCodes.Call, method)

    | A.IdExp(name) ->
        printfn "id exp"

        let label_name =
            match name with
            | A.Val s -> s
            | _ -> failwith "expecting local var"

        let localval = E.Locals.get env.locals label_name
        env.main.Emit(OpCodes.Ldloc, localval)

let eval_dec d (env: E.env) =
    // this function needs access to the main class's ilGenerator
    match d with
    | A.FunDec(binding, fundec) -> // makeFunc at the top level
        printfn "creating function"
        // easy to make function type from this (type list) but how do we call it?
        // make the type returned be abstract
        let { A.outtype = outtype
              A.arg = { A.argname = argname
                        A.type' = intype }
              A.body = body } =
            fundec
        let name =
            match binding with
            | A.Fun x -> x
            | _ -> failwith "expecting a fun name"

        let Argname =
            match argname with
            | A.Val x -> x
            | _ -> failwith "expecting var name"
        let attr = MethodAttributes.Public // ||| MethodAttributes.Static
        // TODO make actaully use types for method constrution
        let f =
            env.prog.DefineMethod(name, attr, CallingConventions.Standard, typeof<int>, [| typeof<int> |])
        let fIL = f.GetILGenerator()
        fIL.Emit(OpCodes.Ldarg_1)

        let argVal = fIL.DeclareLocal(typeof<int>)
        let newLocals = E.Locals.enter Argname argVal env.locals // our single mutation to make the arg in scope of the function
        eval_exp body {env with main=fIL; locals=newLocals}
        printfn "function created"
        env

    | A.VarDec(binding, value) -> // stloc at the top level
        // associate the binding with the result of this exp in the env
        // TODO make a function to initialize all of the globals in the Program constructor
        printfn "vardec"

        let name =
            match binding with
            | A.Val x -> x
            | _ -> failwith "expecting a variable binding"

        let newlocal = env.main.DeclareLocal(typeof<int>)
        eval_exp value env
        env.main.Emit(OpCodes.Stloc, newlocal)

        { env with
            locals = E.Locals.enter name newlocal env.locals }




let eval_stm (env: E.env) =
    function
    | A.Dec(d) -> eval_dec d env
    | A.Exp(e) ->
        eval_exp e env
        env

let eval_prog =
    function
    | A.Prog(stms) ->
        let env = E.initAsm () //initialize assembly
        let new_env = List.fold (fun e -> eval_stm e) env stms //traverse the AST and generate CIL
        E.finishAsm new_env // finalize the assembly and return it for export

    | A.Blank -> failwith "No Statements in program"
