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

type value =
    | Val of type'
    | Fun of abstype * Ast.id * abstype * Ast.exp // only unary functions
