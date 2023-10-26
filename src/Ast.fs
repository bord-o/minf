module Ast

type prog = 
    Prog of stm list    
    | Blank

and id = string

and op =
    Plus
    | Minus
    | Times
    | Eq
    | LT
    | LTE
    | GT
    | GTE

and stm =
    Dec of dec
    | Exp of exp

and dec =
    | FunDec of id * fundec
    | VarDec of id * exp

and exp =
    | OpExp of exp * op * exp
    | NumExp of int
    | IfExp of exp * exp * exp
    | CallExp of id * exp
    | IdExp of id

and fundec = 
    {name: id; outtype:id; args: funargs; body: exp}

and funargs = funarg list

and funarg = 
    {argname: id; type': id}
