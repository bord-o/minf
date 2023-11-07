module CodeGen
open Mono.Cecil.Cil

module A = Ast
module T = Type
module C = CIL

// This module will call out to the CIL module for all il generation functions


module Locals =
    type t = (string * string) list
    let empty : t = []
    let get table search = 
        List.find (fun (k, v) ->  k = search) table |> snd
    let enter k v table =
        (k,v)::table

let rec eval_exp =
    // this function needs access to the the ilGenerator for the containing method
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
            printfn "sub"
        | A.Times -> 
            eval_exp e1
            eval_exp e2
            printfn "mul"

        | A.Neq -> 
            eval_exp e1
            eval_exp e2
            printfn "ceq" // 1 if equal
            printfn "ldc_I4 0"
            printfn "ceq"
        | A.Eq -> 
            eval_exp e1
            eval_exp e2
            printfn "ceq" // 1 if equal
        | A.LT -> 
            eval_exp e1
            eval_exp e2
            printfn "clt" 
        | A.LTE -> 
            eval_exp e1
            printfn "stloc 0" //store left operand
            eval_exp e2
            printfn "stloc 1" //store right operand
            printfn "ldloc 0" //load both operands
            printfn "ldloc 1"
            printfn "clt"  //compare less than (leaves comparison on stack)
            printfn "ldloc 0"
            printfn "ldloc 1"
            printfn "ceq"  //compare equal (leaves comparison on stack)
            // need the top of the stack to be 1,1
            printfn "ldc_I4 1"
            printfn "ceq" //make sure that both comparisons were positive 
            printfn "ceq" 
        | A.GT -> 
            eval_exp e1
            eval_exp e2
            printfn "clt" 
        | A.GTE -> 
            eval_exp e1
            printfn "stloc 0" //store left operand
            eval_exp e2
            printfn "stloc 1" //store right operand
            printfn "ldloc 0" //load both operands
            printfn "ldloc 1"
            printfn "cgt"  //compare less than (leaves comparison on stack)
            printfn "ldloc 0"
            printfn "ldloc 1"
            printfn "ceq"  //compare equal (leaves comparison on stack)
            // need the top of the stack to be 1,1
            printfn "ldc_I4 1"
            printfn "ceq" //make sure that both comparisons were positive 
            printfn "ceq" 

    | A.NumExp(i) -> 
        printfn $"ldc_I4 {i}"
    | A.BoolExp(b) ->
        printfn $"ldc_I4 {if b then 1 else 0}"

    | A.IfExp(if', then', else') -> 
        // to simply compile a conditional, we create a label(instruction) for the else branch 
        // and the end. The end instruction doesn't return, it just leaves the result of the expression 
        // at the top of the stack
        // we can use a small assoc list for the labeled opcodes
        let labels = 
            Locals.empty
            |> Locals.enter "else" "nop"
            |> Locals.enter "end" "nop"

        printfn $"making label else: {eval_exp else'}" // let lables = ("else", il.Create(Nop))::labels
        printfn $"making label end: ldloc 1" // let lables = ("end", il.Create(Ldloc, 1))::labels

        printfn $"{eval_exp if'}"
        printfn $"brfalse  else" //bool

        printfn $"{eval_exp then'}"
        printfn $"stloc 1"
        printfn $"br end"

        printfn "%s" <| Locals.get labels "else"
        printfn $"{eval_exp else'}"
        printfn $"stloc 1"
        printfn $"br end"

        printfn "%s" <| Locals.get labels "end"
        printfn $"ldloc 1"

    | A.CallExp(name, arg) -> printfn "unimplemented" 
    | A.IdExp(name) -> printfn "unimplemented" 

let eval_dec =
    // this function needs access to the main class's ilGenerator
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

        printfn "not implemented"

let eval_stm =
    function
    | A.Dec(d) -> printfn "unimplemented"
    | A.Exp(e) -> eval_exp e  

let eval_prog =
    function
    | A.Prog(stms) -> List.iter (eval_stm) stms
    | A.Blank -> failwith "No Statements in program"

