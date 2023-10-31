module Ast

type prog =
    | Prog of stm list
    | Blank

and stm =
    | Dec of dec
    | Exp of exp

and dec =
    | FunDec of id * fundec
    | VarDec of id * exp

and exp =
    | OpExp of exp * op * exp
    | NumExp of int
    | BoolExp of bool
    | IfExp of exp * exp * exp
    | CallExp of id * exp
    | IdExp of id

and id =
    | Ty of string
    | Fun of string
    | Val of string

and op =
    | Plus
    | Minus
    | Times
    | Eq
    | Neq
    | LT
    | LTE
    | GT
    | GTE

and fundec =
    { name: id
      outtype: id
      arg: funarg
      body: exp }

and funarg = { argname: id; type': id }
