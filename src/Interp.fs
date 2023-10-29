module Interp

module A = Ast
module T = Type

type table = Map<A.id, T.result>

type env =
    { types: table
      variables: table
      functions: table }

let type_of_result (v : T.result) : T.abstype = 
    match v with 
    | T.Val t ->
        match t with
        | T.Int _ -> T.TInt
        | T.Bool _ -> T.TBool
        | T.Unit -> T.TUnit

    | T.Fun (_,i,o) -> T.TFun(i,o)// only unary functions 
    

let eval_id env =
    function
    | A.Ty(name) ->
        let tytable = env.types
        let res = tytable.TryFind(A.Ty name)

        if Option.isNone res then
            failwith "Type not bound"
        else
            res.Value

    | A.Fun(name) ->
        let funtable = env.types
        let res = funtable.TryFind(A.Fun name)

        if Option.isNone res then
            failwith "fun not bound"
        else
            res.Value

    | A.Val(name) ->
        let valtable = env.types
        let res = valtable.TryFind(A.Val name)

        if Option.isNone res then
            failwith "val not bound"
        else
            res.Value

let call env funname args = T.Bool(false)
// make sure fun is in fun table, make sure types are in types table. call the function body exp with the environment
// created from the args/global table

// let typecheck got expect =
//     match got with
//     | T.Int(_) ->
//         match expect with
//         | T.Int(_) -> ()
//         | T.Bool(_) -> failwith $"Expected bool got int"
//     | T.Bool(_) ->
//         match expect with
//         | T.Bool(_) -> ()
//         | T.Int(_) -> failwith $"Expected int got bool"


let checkint i =
    match i with
    | T.Int(x) -> x
    | T.Bool(_) -> failwith $"Expected int got bool"
    | T.Unit -> failwith $"Expected int got unit"

let checkbool b =
    match b with
    | T.Bool(x) -> x
    | T.Int(_) -> failwith $"Expected bool got int"
    | T.Unit -> failwith $"Expected bool got unit"


let rec eval_exp env =
    function
    | A.OpExp(e1, op, e2) ->
        match op with
        // TODO: simple typechecking
        | A.Plus -> T.Int(checkint (eval_exp env e1) + checkint (eval_exp env e2))
        | A.Minus -> T.Int(checkint (eval_exp env e1) - checkint (eval_exp env e2))
        | A.Times -> T.Int(checkint (eval_exp env e1) * checkint (eval_exp env e2))

        | A.Eq -> T.Bool(checkbool (eval_exp env e1) = checkbool (eval_exp env e2))
        | A.LT -> T.Bool(checkbool (eval_exp env e1) < checkbool (eval_exp env e2))
        | A.LTE -> T.Bool(checkbool (eval_exp env e1) <= checkbool (eval_exp env e2))
        | A.GT -> T.Bool(checkbool (eval_exp env e1) > checkbool (eval_exp env e2))
        | A.GTE -> T.Bool(checkbool (eval_exp env e1) >= checkbool (eval_exp env e2))

    | A.NumExp(i) -> T.Int(i)

    | A.IfExp(if', then', else') ->
        if (checkbool (eval_exp env if')) then
            (eval_exp env then')
        else
            (eval_exp env else')
    | A.CallExp(name, args) -> call env name (List.map (fun arg -> eval_exp env arg) args)
    | A.IdExp(name) -> T.Bool(false)

let eval_dec env =
    function
    | A.FunDec(binding, fundec) ->
        // easy to make function type from this (type list) but how do we call it?
        // make the type returned be abstract
        let { A.name = name
              A.outtype = outtype
              A.arg = {A.argname=_; A.type'=intype}
              A.body = body } =
            fundec

        let in' = eval_id env intype |> type_of_result
        let out' = eval_id env outtype |> type_of_result

        let update = Map.add binding (T.Fun(name, in', out')) env.functions
            
        (T.Unit, env)

    | A.VarDec(binding, value) ->
        // associate the binding with the result of this exp in the env
        let res = eval_exp env value
        let update = Map.add binding (T.Val(res)) env.variables
        let new_env = {env with variables=update}
        
        (T.Unit, new_env) 

let eval_stm env =
    function
    | A.Dec(d) -> eval_dec env d
    | A.Exp(e) -> (eval_exp env e, env) //need to pass a copy of env because it doesnt change

let eval_prog env =
    function
    | A.Prog(stms) ->
        let update_env env stm =
            match (eval_stm env stm) with
            | (_, new_env) -> new_env

        List.fold update_env env stms
    | A.Blank -> failwith "No Statements in program"

// We just make everything a function. let bindings are either to functions with arguments or functions(values) without arguments
