// Implementation file for parser generated by fsyacc
module Parser
#nowarn "64";; // turn off warnings that type variables used in production annotations are instantiated to concrete type
open FSharp.Text.Lexing
open FSharp.Text.Parsing.ParseHelpers
# 1 "src/parser/Parser.fsy"

module A = Ast
let pp s = printfn s

# 11 "src/parser/Parser.fs"
// This type is the type of tokens accepted by the parser
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
  | NEQ
// This type is used to give symbolic names to token indexes, useful for error messages
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
    | TOKEN_NEQ
    | TOKEN_end_of_input
    | TOKEN_error
// This type is used to give symbolic names to token indexes, useful for error messages
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

// This function maps tokens to integer indexes
let tagOfToken (t:token) = 
  match t with
  | EOF  -> 0 
  | ID _ -> 1 
  | NUMBER _ -> 2 
  | LET  -> 3 
  | EXPEND  -> 4 
  | ASSIGN  -> 5 
  | OPAREN  -> 6 
  | CPAREN  -> 7 
  | IF  -> 8 
  | THEN  -> 9 
  | ELSE  -> 10 
  | IS  -> 11 
  | COLON  -> 12 
  | FUN  -> 13 
  | PLUS  -> 14 
  | MINUS  -> 15 
  | TIMES  -> 16 
  | LT  -> 17 
  | GT  -> 18 
  | LTE  -> 19 
  | GTE  -> 20 
  | TRUE  -> 21 
  | FALSE  -> 22 
  | NEQ  -> 23 

// This function maps integer indexes to symbolic token ids
let tokenTagToTokenId (tokenIdx:int) = 
  match tokenIdx with
  | 0 -> TOKEN_EOF 
  | 1 -> TOKEN_ID 
  | 2 -> TOKEN_NUMBER 
  | 3 -> TOKEN_LET 
  | 4 -> TOKEN_EXPEND 
  | 5 -> TOKEN_ASSIGN 
  | 6 -> TOKEN_OPAREN 
  | 7 -> TOKEN_CPAREN 
  | 8 -> TOKEN_IF 
  | 9 -> TOKEN_THEN 
  | 10 -> TOKEN_ELSE 
  | 11 -> TOKEN_IS 
  | 12 -> TOKEN_COLON 
  | 13 -> TOKEN_FUN 
  | 14 -> TOKEN_PLUS 
  | 15 -> TOKEN_MINUS 
  | 16 -> TOKEN_TIMES 
  | 17 -> TOKEN_LT 
  | 18 -> TOKEN_GT 
  | 19 -> TOKEN_LTE 
  | 20 -> TOKEN_GTE 
  | 21 -> TOKEN_TRUE 
  | 22 -> TOKEN_FALSE 
  | 23 -> TOKEN_NEQ 
  | 26 -> TOKEN_end_of_input
  | 24 -> TOKEN_error
  | _ -> failwith "tokenTagToTokenId: bad token"

/// This function maps production indexes returned in syntax errors to strings representing the non terminal that would be produced by that production
let prodIdxToNonTerminal (prodIdx:int) = 
  match prodIdx with
    | 0 -> NONTERM__startstart 
    | 1 -> NONTERM_start 
    | 2 -> NONTERM_start 
    | 3 -> NONTERM_stms 
    | 4 -> NONTERM_stms 
    | 5 -> NONTERM_stm 
    | 6 -> NONTERM_stm 
    | 7 -> NONTERM_dec 
    | 8 -> NONTERM_dec 
    | 9 -> NONTERM_fun 
    | 10 -> NONTERM_fun_arg 
    | 11 -> NONTERM_exp 
    | 12 -> NONTERM_exp 
    | 13 -> NONTERM_exp 
    | 14 -> NONTERM_exp 
    | 15 -> NONTERM_exp 
    | 16 -> NONTERM_exp 
    | 17 -> NONTERM_exp 
    | 18 -> NONTERM_if_exp 
    | 19 -> NONTERM_call_exp 
    | 20 -> NONTERM_op_exp 
    | 21 -> NONTERM_op_exp 
    | 22 -> NONTERM_op_exp 
    | 23 -> NONTERM_op_exp 
    | 24 -> NONTERM_op_exp 
    | 25 -> NONTERM_op_exp 
    | 26 -> NONTERM_op_exp 
    | 27 -> NONTERM_op_exp 
    | 28 -> NONTERM_op_exp 
    | 29 -> NONTERM_number_lit 
    | 30 -> NONTERM_bool_lit 
    | 31 -> NONTERM_bool_lit 
    | 32 -> NONTERM_identifier_lit 
    | 33 -> NONTERM_end 
    | _ -> failwith "prodIdxToNonTerminal: bad production index"

let _fsyacc_endOfInputTag = 26 
let _fsyacc_tagOfErrorTerminal = 24

// This function gets the name of a token as a string
let token_to_string (t:token) = 
  match t with 
  | EOF  -> "EOF" 
  | ID _ -> "ID" 
  | NUMBER _ -> "NUMBER" 
  | LET  -> "LET" 
  | EXPEND  -> "EXPEND" 
  | ASSIGN  -> "ASSIGN" 
  | OPAREN  -> "OPAREN" 
  | CPAREN  -> "CPAREN" 
  | IF  -> "IF" 
  | THEN  -> "THEN" 
  | ELSE  -> "ELSE" 
  | IS  -> "IS" 
  | COLON  -> "COLON" 
  | FUN  -> "FUN" 
  | PLUS  -> "PLUS" 
  | MINUS  -> "MINUS" 
  | TIMES  -> "TIMES" 
  | LT  -> "LT" 
  | GT  -> "GT" 
  | LTE  -> "LTE" 
  | GTE  -> "GTE" 
  | TRUE  -> "TRUE" 
  | FALSE  -> "FALSE" 
  | NEQ  -> "NEQ" 

// This function gets the data carried by a token as an object
let _fsyacc_dataOfToken (t:token) = 
  match t with 
  | EOF  -> (null : System.Object) 
  | ID _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | NUMBER _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | LET  -> (null : System.Object) 
  | EXPEND  -> (null : System.Object) 
  | ASSIGN  -> (null : System.Object) 
  | OPAREN  -> (null : System.Object) 
  | CPAREN  -> (null : System.Object) 
  | IF  -> (null : System.Object) 
  | THEN  -> (null : System.Object) 
  | ELSE  -> (null : System.Object) 
  | IS  -> (null : System.Object) 
  | COLON  -> (null : System.Object) 
  | FUN  -> (null : System.Object) 
  | PLUS  -> (null : System.Object) 
  | MINUS  -> (null : System.Object) 
  | TIMES  -> (null : System.Object) 
  | LT  -> (null : System.Object) 
  | GT  -> (null : System.Object) 
  | LTE  -> (null : System.Object) 
  | GTE  -> (null : System.Object) 
  | TRUE  -> (null : System.Object) 
  | FALSE  -> (null : System.Object) 
  | NEQ  -> (null : System.Object) 
let _fsyacc_gotos = [| 0us;65535us;1us;65535us;0us;1us;2us;65535us;0us;2us;5us;6us;2us;65535us;0us;5us;5us;5us;2us;65535us;0us;7us;5us;7us;1us;65535us;13us;15us;1us;65535us;18us;19us;18us;65535us;0us;9us;5us;9us;13us;14us;23us;24us;28us;29us;37us;38us;39us;40us;41us;42us;43us;44us;54us;45us;55us;46us;56us;47us;57us;48us;58us;49us;59us;50us;60us;51us;61us;52us;62us;53us;18us;65535us;0us;36us;5us;36us;13us;36us;23us;36us;28us;36us;37us;36us;39us;36us;41us;36us;43us;36us;54us;36us;55us;36us;56us;36us;57us;36us;58us;36us;59us;36us;60us;36us;61us;36us;62us;36us;18us;65535us;0us;35us;5us;35us;13us;35us;23us;35us;28us;35us;37us;35us;39us;35us;41us;35us;43us;35us;54us;35us;55us;35us;56us;35us;57us;35us;58us;35us;59us;35us;60us;35us;61us;35us;62us;35us;18us;65535us;0us;34us;5us;34us;13us;34us;23us;34us;28us;34us;37us;34us;39us;34us;41us;34us;43us;34us;54us;34us;55us;34us;56us;34us;57us;34us;58us;34us;59us;34us;60us;34us;61us;34us;62us;34us;18us;65535us;0us;31us;5us;31us;13us;31us;23us;31us;28us;31us;37us;31us;39us;31us;41us;31us;43us;31us;54us;31us;55us;31us;56us;31us;57us;31us;58us;31us;59us;31us;60us;31us;61us;31us;62us;31us;18us;65535us;0us;32us;5us;32us;13us;32us;23us;32us;28us;32us;37us;32us;39us;32us;41us;32us;43us;32us;54us;32us;55us;32us;56us;32us;57us;32us;58us;32us;59us;32us;60us;32us;61us;32us;62us;32us;18us;65535us;0us;33us;5us;33us;13us;33us;23us;33us;28us;33us;37us;33us;39us;33us;41us;33us;43us;33us;54us;33us;55us;33us;56us;33us;57us;33us;58us;33us;59us;33us;60us;33us;61us;33us;62us;33us;2us;65535us;0us;4us;2us;3us;|]
let _fsyacc_sparseGotoTableRowOffsets = [|0us;1us;3us;6us;9us;12us;14us;16us;35us;54us;73us;92us;111us;130us;149us;|]
let _fsyacc_stateToProdIdxsTableElements = [| 1us;0us;1us;0us;1us;1us;1us;1us;1us;2us;2us;3us;4us;1us;3us;1us;5us;1us;5us;10us;6us;20us;21us;22us;23us;24us;25us;26us;27us;28us;1us;6us;2us;7us;8us;2us;7us;8us;2us;7us;8us;10us;7us;20us;21us;22us;23us;24us;25us;26us;27us;28us;1us;8us;1us;9us;1us;9us;1us;9us;1us;9us;1us;9us;1us;9us;1us;9us;1us;9us;10us;9us;20us;21us;22us;23us;24us;25us;26us;27us;28us;1us;10us;1us;10us;1us;10us;1us;11us;10us;11us;20us;21us;22us;23us;24us;25us;26us;27us;28us;1us;11us;1us;12us;1us;13us;1us;14us;1us;15us;1us;16us;1us;17us;1us;18us;10us;18us;20us;21us;22us;23us;24us;25us;26us;27us;28us;1us;18us;10us;18us;20us;21us;22us;23us;24us;25us;26us;27us;28us;1us;18us;10us;18us;20us;21us;22us;23us;24us;25us;26us;27us;28us;2us;19us;32us;10us;19us;20us;21us;22us;23us;24us;25us;26us;27us;28us;10us;20us;20us;21us;22us;23us;24us;25us;26us;27us;28us;10us;20us;21us;21us;22us;23us;24us;25us;26us;27us;28us;10us;20us;21us;22us;22us;23us;24us;25us;26us;27us;28us;10us;20us;21us;22us;23us;23us;24us;25us;26us;27us;28us;10us;20us;21us;22us;23us;24us;24us;25us;26us;27us;28us;10us;20us;21us;22us;23us;24us;25us;25us;26us;27us;28us;10us;20us;21us;22us;23us;24us;25us;26us;26us;27us;28us;10us;20us;21us;22us;23us;24us;25us;26us;27us;27us;28us;10us;20us;21us;22us;23us;24us;25us;26us;27us;28us;28us;1us;20us;1us;21us;1us;22us;1us;23us;1us;24us;1us;25us;1us;26us;1us;27us;1us;28us;1us;29us;1us;30us;1us;31us;1us;33us;|]
let _fsyacc_stateToProdIdxsTableRowOffsets = [|0us;2us;4us;6us;8us;10us;13us;15us;17us;19us;30us;32us;35us;38us;41us;52us;54us;56us;58us;60us;62us;64us;66us;68us;70us;81us;83us;85us;87us;89us;100us;102us;104us;106us;108us;110us;112us;114us;116us;127us;129us;140us;142us;153us;156us;167us;178us;189us;200us;211us;222us;233us;244us;255us;266us;268us;270us;272us;274us;276us;278us;280us;282us;284us;286us;288us;290us;|]
let _fsyacc_action_rows = 67
let _fsyacc_actionTableElements = [|8us;32768us;0us;66us;1us;43us;2us;63us;3us;11us;6us;28us;8us;37us;21us;64us;22us;65us;0us;49152us;1us;32768us;0us;66us;0us;16385us;0us;16386us;7us;16388us;1us;43us;2us;63us;3us;11us;6us;28us;8us;37us;21us;64us;22us;65us;0us;16387us;1us;32768us;4us;8us;0us;16389us;10us;32768us;4us;10us;5us;57us;14us;54us;15us;55us;16us;56us;17us;59us;18us;61us;19us;60us;20us;62us;23us;58us;0us;16390us;1us;32768us;1us;12us;1us;32768us;5us;13us;7us;32768us;1us;43us;2us;63us;6us;28us;8us;37us;13us;16us;21us;64us;22us;65us;9us;16391us;5us;57us;14us;54us;15us;55us;16us;56us;17us;59us;18us;61us;19us;60us;20us;62us;23us;58us;0us;16392us;1us;32768us;1us;17us;1us;32768us;6us;18us;1us;32768us;1us;25us;1us;32768us;7us;20us;1us;32768us;12us;21us;1us;32768us;1us;22us;1us;32768us;11us;23us;6us;32768us;1us;43us;2us;63us;6us;28us;8us;37us;21us;64us;22us;65us;9us;16393us;5us;57us;14us;54us;15us;55us;16us;56us;17us;59us;18us;61us;19us;60us;20us;62us;23us;58us;1us;32768us;12us;26us;1us;32768us;1us;27us;0us;16394us;6us;32768us;1us;43us;2us;63us;6us;28us;8us;37us;21us;64us;22us;65us;10us;32768us;5us;57us;7us;30us;14us;54us;15us;55us;16us;56us;17us;59us;18us;61us;19us;60us;20us;62us;23us;58us;0us;16395us;0us;16396us;0us;16397us;0us;16398us;0us;16399us;0us;16400us;0us;16401us;6us;32768us;1us;43us;2us;63us;6us;28us;8us;37us;21us;64us;22us;65us;10us;32768us;5us;57us;9us;39us;14us;54us;15us;55us;16us;56us;17us;59us;18us;61us;19us;60us;20us;62us;23us;58us;6us;32768us;1us;43us;2us;63us;6us;28us;8us;37us;21us;64us;22us;65us;10us;32768us;5us;57us;10us;41us;14us;54us;15us;55us;16us;56us;17us;59us;18us;61us;19us;60us;20us;62us;23us;58us;6us;32768us;1us;43us;2us;63us;6us;28us;8us;37us;21us;64us;22us;65us;0us;16402us;6us;16416us;1us;43us;2us;63us;6us;28us;8us;37us;21us;64us;22us;65us;0us;16403us;0us;16404us;0us;16405us;0us;16406us;0us;16407us;0us;16408us;0us;16409us;0us;16410us;0us;16411us;0us;16412us;6us;32768us;1us;43us;2us;63us;6us;28us;8us;37us;21us;64us;22us;65us;6us;32768us;1us;43us;2us;63us;6us;28us;8us;37us;21us;64us;22us;65us;6us;32768us;1us;43us;2us;63us;6us;28us;8us;37us;21us;64us;22us;65us;6us;32768us;1us;43us;2us;63us;6us;28us;8us;37us;21us;64us;22us;65us;6us;32768us;1us;43us;2us;63us;6us;28us;8us;37us;21us;64us;22us;65us;6us;32768us;1us;43us;2us;63us;6us;28us;8us;37us;21us;64us;22us;65us;6us;32768us;1us;43us;2us;63us;6us;28us;8us;37us;21us;64us;22us;65us;6us;32768us;1us;43us;2us;63us;6us;28us;8us;37us;21us;64us;22us;65us;6us;32768us;1us;43us;2us;63us;6us;28us;8us;37us;21us;64us;22us;65us;0us;16413us;0us;16414us;0us;16415us;0us;16417us;|]
let _fsyacc_actionTableRowOffsets = [|0us;9us;10us;12us;13us;14us;22us;23us;25us;26us;37us;38us;40us;42us;50us;60us;61us;63us;65us;67us;69us;71us;73us;75us;82us;92us;94us;96us;97us;104us;115us;116us;117us;118us;119us;120us;121us;122us;129us;140us;147us;158us;165us;166us;173us;174us;175us;176us;177us;178us;179us;180us;181us;182us;183us;190us;197us;204us;211us;218us;225us;232us;239us;246us;247us;248us;249us;|]
let _fsyacc_reductionSymbolCounts = [|1us;2us;1us;2us;1us;2us;2us;4us;4us;9us;3us;3us;1us;1us;1us;1us;1us;1us;6us;2us;3us;3us;3us;3us;3us;3us;3us;3us;3us;1us;1us;1us;1us;1us;|]
let _fsyacc_productionToNonTerminalTable = [|0us;1us;1us;2us;2us;3us;3us;4us;4us;5us;6us;7us;7us;7us;7us;7us;7us;7us;8us;9us;10us;10us;10us;10us;10us;10us;10us;10us;10us;11us;12us;12us;13us;14us;|]
let _fsyacc_immediateActions = [|65535us;49152us;65535us;16385us;16386us;65535us;16387us;65535us;16389us;65535us;16390us;65535us;65535us;65535us;65535us;16392us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;16394us;65535us;65535us;16395us;16396us;16397us;16398us;16399us;16400us;16401us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;16413us;16414us;16415us;16417us;|]
let _fsyacc_reductions = lazy [|
# 251 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> Ast.prog in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
                      raise (FSharp.Text.Parsing.Accept(Microsoft.FSharp.Core.Operators.box _1))
                   )
                 : 'gentype__startstart));
# 260 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_stms in
            let _2 = parseState.GetInput(2) :?> 'gentype_end in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 31 "src/parser/Parser.fsy"
                                       A.Prog(_1) 
                   )
# 31 "src/parser/Parser.fsy"
                 : Ast.prog));
# 272 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_end in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 32 "src/parser/Parser.fsy"
                                       A.Blank 
                   )
# 32 "src/parser/Parser.fsy"
                 : Ast.prog));
# 283 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_stm in
            let _2 = parseState.GetInput(2) :?> 'gentype_stms in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 35 "src/parser/Parser.fsy"
                                     pp "stms"; _1::_2
                   )
# 35 "src/parser/Parser.fsy"
                 : 'gentype_stms));
# 295 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_stm in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 36 "src/parser/Parser.fsy"
                                _1::[]
                   )
# 36 "src/parser/Parser.fsy"
                 : 'gentype_stms));
# 306 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_dec in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 39 "src/parser/Parser.fsy"
                                       pp "dec"; A.Dec(_1)
                   )
# 39 "src/parser/Parser.fsy"
                 : 'gentype_stm));
# 317 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 40 "src/parser/Parser.fsy"
                                       pp "exp"; A.Exp(_1)
                   )
# 40 "src/parser/Parser.fsy"
                 : 'gentype_stm));
# 328 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = parseState.GetInput(2) :?> string in
            let _4 = parseState.GetInput(4) :?> 'gentype_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 43 "src/parser/Parser.fsy"
                                              pp "let"; A.VarDec(A.Val(_2), _4)
                   )
# 43 "src/parser/Parser.fsy"
                 : 'gentype_dec));
# 340 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = parseState.GetInput(2) :?> string in
            let _4 = parseState.GetInput(4) :?> 'gentype_fun in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 44 "src/parser/Parser.fsy"
                                              pp "fun"; A.FunDec(A.Fun(_2), _4)
                   )
# 44 "src/parser/Parser.fsy"
                 : 'gentype_dec));
# 352 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = parseState.GetInput(2) :?> string in
            let _4 = parseState.GetInput(4) :?> 'gentype_fun_arg in
            let _7 = parseState.GetInput(7) :?> string in
            let _9 = parseState.GetInput(9) :?> 'gentype_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 47 "src/parser/Parser.fsy"
                                                                          pp "fun declaration"; {name=A.Fun(_2); outtype=A.Ty(_7); arg= _4; body= _9} 
                   )
# 47 "src/parser/Parser.fsy"
                 : 'gentype_fun));
# 366 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> string in
            let _3 = parseState.GetInput(3) :?> string in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 50 "src/parser/Parser.fsy"
                                        pp "funarg"; {argname=A.Val(_1); type'=A.Ty(_3)}
                   )
# 50 "src/parser/Parser.fsy"
                 : 'gentype_fun_arg));
# 378 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = parseState.GetInput(2) :?> 'gentype_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 53 "src/parser/Parser.fsy"
                                              _2
                   )
# 53 "src/parser/Parser.fsy"
                 : 'gentype_exp));
# 389 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_number_lit in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 54 "src/parser/Parser.fsy"
                                       _1
                   )
# 54 "src/parser/Parser.fsy"
                 : 'gentype_exp));
# 400 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_bool_lit in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 55 "src/parser/Parser.fsy"
                                     _1
                   )
# 55 "src/parser/Parser.fsy"
                 : 'gentype_exp));
# 411 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_identifier_lit in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 56 "src/parser/Parser.fsy"
                                           _1
                   )
# 56 "src/parser/Parser.fsy"
                 : 'gentype_exp));
# 422 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_op_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 57 "src/parser/Parser.fsy"
                                   _1
                   )
# 57 "src/parser/Parser.fsy"
                 : 'gentype_exp));
# 433 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_call_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 58 "src/parser/Parser.fsy"
                                     _1
                   )
# 58 "src/parser/Parser.fsy"
                 : 'gentype_exp));
# 444 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_if_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 59 "src/parser/Parser.fsy"
                                   _1
                   )
# 59 "src/parser/Parser.fsy"
                 : 'gentype_exp));
# 455 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _2 = parseState.GetInput(2) :?> 'gentype_exp in
            let _4 = parseState.GetInput(4) :?> 'gentype_exp in
            let _6 = parseState.GetInput(6) :?> 'gentype_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 62 "src/parser/Parser.fsy"
                                                                pp "if then else"; A.IfExp(_2, _4, _6)
                   )
# 62 "src/parser/Parser.fsy"
                 : 'gentype_if_exp));
# 468 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> string in
            let _2 = parseState.GetInput(2) :?> 'gentype_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 65 "src/parser/Parser.fsy"
                                   pp "function call"; A.CallExp(A.Fun(_1), _2)
                   )
# 65 "src/parser/Parser.fsy"
                 : 'gentype_call_exp));
# 480 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_exp in
            let _3 = parseState.GetInput(3) :?> 'gentype_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 68 "src/parser/Parser.fsy"
                                         pp "plus"; A.OpExp(_1,A.Plus,_3)
                   )
# 68 "src/parser/Parser.fsy"
                 : 'gentype_op_exp));
# 492 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_exp in
            let _3 = parseState.GetInput(3) :?> 'gentype_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 69 "src/parser/Parser.fsy"
                                          pp "minus"; A.OpExp(_1,A.Minus,_3)
                   )
# 69 "src/parser/Parser.fsy"
                 : 'gentype_op_exp));
# 504 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_exp in
            let _3 = parseState.GetInput(3) :?> 'gentype_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 70 "src/parser/Parser.fsy"
                                          pp "times"; A.OpExp(_1,A.Times,_3)
                   )
# 70 "src/parser/Parser.fsy"
                 : 'gentype_op_exp));
# 516 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_exp in
            let _3 = parseState.GetInput(3) :?> 'gentype_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 72 "src/parser/Parser.fsy"
                                           pp "equals"; A.OpExp(_1,A.Eq,_3)
                   )
# 72 "src/parser/Parser.fsy"
                 : 'gentype_op_exp));
# 528 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_exp in
            let _3 = parseState.GetInput(3) :?> 'gentype_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 73 "src/parser/Parser.fsy"
                                        pp "neq"; A.OpExp(_1, A.Neq, _3)
                   )
# 73 "src/parser/Parser.fsy"
                 : 'gentype_op_exp));
# 540 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_exp in
            let _3 = parseState.GetInput(3) :?> 'gentype_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 74 "src/parser/Parser.fsy"
                                       pp "lt"; A.OpExp(_1,A.LT,_3)
                   )
# 74 "src/parser/Parser.fsy"
                 : 'gentype_op_exp));
# 552 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_exp in
            let _3 = parseState.GetInput(3) :?> 'gentype_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 75 "src/parser/Parser.fsy"
                                        pp "lte"; A.OpExp(_1,A.LTE,_3)
                   )
# 75 "src/parser/Parser.fsy"
                 : 'gentype_op_exp));
# 564 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_exp in
            let _3 = parseState.GetInput(3) :?> 'gentype_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 76 "src/parser/Parser.fsy"
                                       pp "gt"; A.OpExp(_1,A.GT,_3)
                   )
# 76 "src/parser/Parser.fsy"
                 : 'gentype_op_exp));
# 576 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_exp in
            let _3 = parseState.GetInput(3) :?> 'gentype_exp in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 77 "src/parser/Parser.fsy"
                                        pp "gte"; A.OpExp(_1,A.GTE,_3)
                   )
# 77 "src/parser/Parser.fsy"
                 : 'gentype_op_exp));
# 588 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> int in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 80 "src/parser/Parser.fsy"
                                   pp "number"; A.NumExp(_1)
                   )
# 80 "src/parser/Parser.fsy"
                 : 'gentype_number_lit));
# 599 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 83 "src/parser/Parser.fsy"
                                 pp "true"; A.BoolExp(true)
                   )
# 83 "src/parser/Parser.fsy"
                 : 'gentype_bool_lit));
# 609 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 84 "src/parser/Parser.fsy"
                                  pp "true"; A.BoolExp(false)
                   )
# 84 "src/parser/Parser.fsy"
                 : 'gentype_bool_lit));
# 619 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> string in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 87 "src/parser/Parser.fsy"
                               pp "identifier"; A.IdExp(A.Val(_1))
                   )
# 87 "src/parser/Parser.fsy"
                 : 'gentype_identifier_lit));
# 630 "src/parser/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 91 "src/parser/Parser.fsy"
                                printfn "EOF"
                   )
# 91 "src/parser/Parser.fsy"
                 : 'gentype_end));
|]
# 641 "src/parser/Parser.fs"
let tables : FSharp.Text.Parsing.Tables<_> = 
  { reductions = _fsyacc_reductions.Value;
    endOfInputTag = _fsyacc_endOfInputTag;
    tagOfToken = tagOfToken;
    dataOfToken = _fsyacc_dataOfToken; 
    actionTableElements = _fsyacc_actionTableElements;
    actionTableRowOffsets = _fsyacc_actionTableRowOffsets;
    stateToProdIdxsTableElements = _fsyacc_stateToProdIdxsTableElements;
    stateToProdIdxsTableRowOffsets = _fsyacc_stateToProdIdxsTableRowOffsets;
    reductionSymbolCounts = _fsyacc_reductionSymbolCounts;
    immediateActions = _fsyacc_immediateActions;
    gotos = _fsyacc_gotos;
    sparseGotoTableRowOffsets = _fsyacc_sparseGotoTableRowOffsets;
    tagOfErrorTerminal = _fsyacc_tagOfErrorTerminal;
    parseError = (fun (ctxt:FSharp.Text.Parsing.ParseErrorContext<_>) -> 
                              match parse_error_rich with 
                              | Some f -> f ctxt
                              | None -> parse_error ctxt.Message);
    numTerminals = 27;
    productionToNonTerminalTable = _fsyacc_productionToNonTerminalTable  }
let engine lexer lexbuf startState = tables.Interpret(lexer, lexbuf, startState)
let start lexer lexbuf : Ast.prog =
    engine lexer lexbuf 0 :?> _
