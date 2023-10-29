module Type

type type' =
    | Int of int
    | Bool of bool
    | Unit

type abstype =
    | TInt
    | TBool
    | TUnit
    | TFun of abstype * abstype

type result =
    | Val of type'
    | Fun of Ast.id * abstype * abstype // only unary functions
