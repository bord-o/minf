module Interp

module A = Ast
let eval_exp (e: A.exp) = function
    | OpExp(e1, op, e2) -> () 
    | NumExp(i) ->() 
    | IfExp(if', then', else')  -> ()
    | CallExp(name, args) -> () 
    | IdExp(name) -> () 