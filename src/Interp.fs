module Interp

module A = Ast
module T = Type

type table = Map<A.id, T.value>

type env =
    { types: Map<A.id, T.abstype>
      variables: table
      functions: table }

let init_env =
    { types = Map.add (A.Ty "bool") T.TBool (Map.add (A.Ty "int") T.TInt Map.empty<A.id, T.abstype>)
      variables = Map.empty<A.id, T.value>
      functions = Map.empty<A.id, T.value> }


let rec type_of_result (v: T.value) : T.abstype =
    match v with
    | T.Val t ->
        match t with
        | T.Int _ -> T.TInt
        | T.Bool _ -> T.TBool
        | T.Unit -> T.TUnit

    | T.Fun(i,_, o, _) -> T.TFun(i, o) // only unary functions

//eval_ty_id -> abstype
let eval_ty_id env =
    function
    | A.Ty(name) ->
        let tytable = env.types
        let res = tytable.TryFind(A.Ty name)

        if Option.isNone res then
            failwith "Type not bound"
        else
            res.Value
    | _ -> failwith "expected type"

//eval_fun_id -> value (Fun)
let eval_fun_id env =
    function
    | A.Fun(name) ->
        let funtable = env.functions
        let res = funtable.TryFind(A.Fun name)

        if Option.isNone res then
            failwith "fun not bound"
        else
            res.Value
    | _ -> failwith "Expected fun"

//eval_val_id -> type'
let eval_val_id env =
    function
    | A.Val(name) ->
        let valtable = env.variables
        let res = valtable.TryFind(A.Val name)

        if Option.isNone res then
            failwith "val not bound"
        else
            match res.Value with
            | T.Val ty -> ty
            | _ -> failwith "Expected value not function"
    | _ -> failwith "Expected value"


// let typecheck got expect =k
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
        //printfn $"checking plus {eval_exp env e1}, {eval_exp env e2}";
        | A.Plus -> T.Int(checkint (eval_exp env e1) + checkint (eval_exp env e2))
        | A.Minus -> T.Int(checkint (eval_exp env e1) - checkint (eval_exp env e2))
        | A.Times -> T.Int(checkint (eval_exp env e1) * checkint (eval_exp env e2))

        | A.Neq -> T.Bool(checkint (eval_exp env e1) <> checkint (eval_exp env e2))
        | A.Eq -> T.Bool(checkint (eval_exp env e1) = checkint (eval_exp env e2))
        | A.LT -> T.Bool(checkint (eval_exp env e1) < checkint (eval_exp env e2))
        | A.LTE -> T.Bool(checkint (eval_exp env e1) <= checkint (eval_exp env e2))
        | A.GT -> T.Bool(checkint (eval_exp env e1) > checkint (eval_exp env e2))
        | A.GTE -> T.Bool(checkint (eval_exp env e1) >= checkint (eval_exp env e2))

    | A.NumExp(i) -> T.Int(i)
    | A.BoolExp(b) -> T.Bool(b)

    | A.IfExp(if', then', else') ->
    //TODO: why is my if eager to call a recursive function in the else branch but not the then branch?
        if (checkbool (eval_exp env if')) then
            (eval_exp env then')
        else
            (eval_exp env else')
    | A.CallExp(name, arg) -> call env name (eval_exp env arg)
    | A.IdExp(name) -> eval_val_id env name

and call (env: env) (funname: A.id) (arg: T.type') = 
    // find the local arg name of the function's exp, then bind the passed arg
    // to the local env that this function uses. Evaluate that exp and return the unchanged environment.
    // TODO type check this stuff
    //printfn "Callable functions: %A" env.functions
    //printfn "looking up function: %A" funname
    //printfn $"Calling {funname} with {arg}"
    //printfn $"Vars: {env.variables}"

    let arg_name, body = 
        match env.functions.TryFind funname with
        | Some(fun') ->
            match fun' with
            | T.Fun (in', arg, out', body) -> (arg, body) 
            | _ -> failwith "Expected function, not value/type"
        | None -> failwith "Function not found"

    let local_env = {env with variables=Map.add arg_name (T.Val arg) env.variables}
    let result = eval_exp local_env body
    result


    // make sure fun is in fun table, make sure types are in types table. call the function body exp with the environment
    // created from the args/global table

let eval_dec env =
    function
    | A.FunDec(binding, fundec) ->
        // easy to make function type from this (type list) but how do we call it?
        // make the type returned be abstract
        // TODO make recursive functions work
        let { A.outtype = outtype
              A.arg = { A.argname = argname; A.type' = intype }
              A.body = body } =
            fundec

        let in' = eval_ty_id env intype
        let out' = eval_ty_id env outtype

        let update = Map.add binding (T.Fun(in', argname, out', body)) env.functions
        let new_env = {env with functions = update}

        (T.Unit, new_env)

    | A.VarDec(binding, value) ->
        // associate the binding with the result of this exp in the env
        let res = eval_exp env value
        let update = Map.add binding (T.Val(res)) env.variables
        let new_env = { env with variables = update }

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
            | (res, new_env) ->
                printfn "Result: %A" res
                //printfn "Vars: %A" new_env.variables
                //printfn "Tys: %A" new_env.types
                //printfn "Funs: %A" new_env.functions
                new_env

        List.fold update_env env stms
    | A.Blank -> failwith "No Statements in program"

// We just make everything a function. let bindings are either to functions with arguments or functions(values) without arguments
