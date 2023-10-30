// Signature file for parser generated by fsyacc
module Parser
type token = 
  | EOF
  | ID of (string)
  | NUMBER of (int)
  | LET
  | EXPEND
  | ASSIGN
  | OPAREN
  | CPAREN
  | IF
  | THEN
  | ELSE
  | IS
  | COLON
  | FUN
  | PLUS
  | MINUS
  | TIMES
  | LT
  | GT
  | LTE
  | GTE
  | TRUE
  | FALSE
type tokenId = 
    | TOKEN_EOF
    | TOKEN_ID
    | TOKEN_NUMBER
    | TOKEN_LET
    | TOKEN_EXPEND
    | TOKEN_ASSIGN
    | TOKEN_OPAREN
    | TOKEN_CPAREN
    | TOKEN_IF
    | TOKEN_THEN
    | TOKEN_ELSE
    | TOKEN_IS
    | TOKEN_COLON
    | TOKEN_FUN
    | TOKEN_PLUS
    | TOKEN_MINUS
    | TOKEN_TIMES
    | TOKEN_LT
    | TOKEN_GT
    | TOKEN_LTE
    | TOKEN_GTE
    | TOKEN_TRUE
    | TOKEN_FALSE
    | TOKEN_end_of_input
    | TOKEN_error
type nonTerminalId = 
    | NONTERM__startstart
    | NONTERM_start
    | NONTERM_stms
    | NONTERM_stm
    | NONTERM_dec
    | NONTERM_fun
    | NONTERM_fun_arg
    | NONTERM_exp
    | NONTERM_if_exp
    | NONTERM_call_exp
    | NONTERM_op_exp
    | NONTERM_number_lit
    | NONTERM_bool_lit
    | NONTERM_identifier_lit
    | NONTERM_end
/// This function maps tokens to integer indexes
val tagOfToken: token -> int

/// This function maps integer indexes to symbolic token ids
val tokenTagToTokenId: int -> tokenId

/// This function maps production indexes returned in syntax errors to strings representing the non terminal that would be produced by that production
val prodIdxToNonTerminal: int -> nonTerminalId

/// This function gets the name of a token as a string
val token_to_string: token -> string
val start : (FSharp.Text.Lexing.LexBuffer<'cty> -> token) -> FSharp.Text.Lexing.LexBuffer<'cty> -> (Ast.prog) 