module CodeGen

module A = Ast
module T = Type


let rec eval_exp =
    function
    | A.OpExp(e1, op, e2) ->
        match op with

        // emit ldc_I4 (eval_exp env e1)
        // emit ldc_I4 (eval_exp env e2)
        // emit add
        | A.Plus -> 
            eval_exp e1
            eval_exp e2
            printfn "add"
        | A.Minus -> 
            eval_exp e1
            eval_exp e2
            printfn "add"
        | A.Times -> 
            eval_exp e1
            eval_exp e2
            printfn "add"

        | A.Neq -> printfn "unimplemented"
        | A.Eq -> printfn "unimplemented"
        | A.LT -> printfn "unimplemented"
        | A.LTE -> printfn "unimplemented"
        | A.GT -> printfn "unimplemented"
        | A.GTE -> printfn "unimplemented"

    | A.NumExp(i) -> 
        printfn $"ldc_I4 {i}"
    | A.BoolExp(b) ->
        printfn $"ldc_I4 {if b then 1 else 0}"

    | A.IfExp(if', then', else') -> 
        printfn $"making label else: {eval_exp else'}"

        printfn $"brfalse {eval_exp if'} else" //bool
        printfn $"{eval_exp then'}"
        printfn $"else: {eval_exp else'}"

    | A.CallExp(name, arg) -> printfn "unimplemented" 
    | A.IdExp(name) ->printfn "unimplemented" 

let eval_dec env =
    function
    | A.FunDec(binding, fundec) -> // makeFunc at the top level
        // easy to make function type from this (type list) but how do we call it?
        // make the type returned be abstract
        // TODO make recursive functions work
        let { A.outtype = outtype
              A.arg = { A.argname = argname; A.type' = intype }
              A.body = body } =
            fundec

        printfn "not implemented"

    | A.VarDec(binding, value) -> // stloc at the top level
        // associate the binding with the result of this exp in the env
        let res = eval_exp env value
        let update = Map.add binding (T.Val(res)) env.variables
        let new_env = { env with variables = update }

        printfn "not implemented"

let eval_stm =
    function
    | A.Dec(d) -> printfn "unimplemented"
    | A.Exp(e) -> eval_exp e  

let eval_prog =
    function
    | A.Prog(stms) -> List.iter (eval_stm) stms
    | A.Blank -> failwith "No Statements in program"

