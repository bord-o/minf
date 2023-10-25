// Implementation file for parser generated by fsyacc
module Parser
#nowarn "64";; // turn off warnings that type variables used in production annotations are instantiated to concrete type
open FSharp.Text.Lexing
open FSharp.Text.Parsing.ParseHelpers
# 1 "src/Parser.fsy"




# 11 "src/Parser.fs"
// This type is the type of tokens accepted by the parser
type token = 
  | EOF
  | HELLO
// This type is used to give symbolic names to token indexes, useful for error messages
type tokenId = 
    | TOKEN_EOF
    | TOKEN_HELLO
    | TOKEN_end_of_input
    | TOKEN_error
// This type is used to give symbolic names to token indexes, useful for error messages
type nonTerminalId = 
    | NONTERM__startstart
    | NONTERM_start
    | NONTERM_File
    | NONTERM_end

// This function maps tokens to integer indexes
let tagOfToken (t:token) = 
  match t with
  | EOF  -> 0 
  | HELLO  -> 1 

// This function maps integer indexes to symbolic token ids
let tokenTagToTokenId (tokenIdx:int) = 
  match tokenIdx with
  | 0 -> TOKEN_EOF 
  | 1 -> TOKEN_HELLO 
  | 4 -> TOKEN_end_of_input
  | 2 -> TOKEN_error
  | _ -> failwith "tokenTagToTokenId: bad token"

/// This function maps production indexes returned in syntax errors to strings representing the non terminal that would be produced by that production
let prodIdxToNonTerminal (prodIdx:int) = 
  match prodIdx with
    | 0 -> NONTERM__startstart 
    | 1 -> NONTERM_start 
    | 2 -> NONTERM_start 
    | 3 -> NONTERM_File 
    | 4 -> NONTERM_File 
    | 5 -> NONTERM_end 
    | _ -> failwith "prodIdxToNonTerminal: bad production index"

let _fsyacc_endOfInputTag = 4 
let _fsyacc_tagOfErrorTerminal = 2

// This function gets the name of a token as a string
let token_to_string (t:token) = 
  match t with 
  | EOF  -> "EOF" 
  | HELLO  -> "HELLO" 

// This function gets the data carried by a token as an object
let _fsyacc_dataOfToken (t:token) = 
  match t with 
  | EOF  -> (null : System.Object) 
  | HELLO  -> (null : System.Object) 
let _fsyacc_gotos = [| 0us;65535us;1us;65535us;0us;1us;1us;65535us;0us;2us;2us;65535us;0us;4us;2us;3us;|]
let _fsyacc_sparseGotoTableRowOffsets = [|0us;1us;3us;5us;|]
let _fsyacc_stateToProdIdxsTableElements = [| 1us;0us;1us;0us;1us;1us;1us;1us;1us;2us;2us;3us;4us;1us;4us;1us;5us;|]
let _fsyacc_stateToProdIdxsTableRowOffsets = [|0us;2us;4us;6us;8us;10us;13us;15us;|]
let _fsyacc_action_rows = 8
let _fsyacc_actionTableElements = [|2us;32768us;0us;7us;1us;5us;0us;49152us;1us;32768us;0us;7us;0us;16385us;0us;16386us;1us;16387us;1us;6us;0us;16388us;0us;16389us;|]
let _fsyacc_actionTableRowOffsets = [|0us;3us;4us;6us;7us;8us;10us;11us;|]
let _fsyacc_reductionSymbolCounts = [|1us;2us;1us;1us;2us;1us;|]
let _fsyacc_productionToNonTerminalTable = [|0us;1us;1us;2us;2us;3us;|]
let _fsyacc_immediateActions = [|65535us;49152us;65535us;16385us;16386us;65535us;16388us;16389us;|]
let _fsyacc_reductions = lazy [|
# 80 "src/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?>  int  in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
                      raise (FSharp.Text.Parsing.Accept(Microsoft.FSharp.Core.Operators.box _1))
                   )
                 : 'gentype__startstart));
# 89 "src/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_File in
            let _2 = parseState.GetInput(2) :?> 'gentype_end in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 23 "src/Parser.fsy"
                                       _1 
                   )
# 23 "src/Parser.fsy"
                 :  int ));
# 101 "src/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            let _1 = parseState.GetInput(1) :?> 'gentype_end in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 24 "src/Parser.fsy"
                                       _1 
                   )
# 24 "src/Parser.fsy"
                 :  int ));
# 112 "src/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 27 "src/Parser.fsy"
                                     1 
                   )
# 27 "src/Parser.fsy"
                 : 'gentype_File));
# 122 "src/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 28 "src/Parser.fsy"
                                         2 
                   )
# 28 "src/Parser.fsy"
                 : 'gentype_File));
# 132 "src/Parser.fs"
        (fun (parseState : FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 32 "src/Parser.fsy"
                                3 
                   )
# 32 "src/Parser.fsy"
                 : 'gentype_end));
|]
# 143 "src/Parser.fs"
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
    numTerminals = 5;
    productionToNonTerminalTable = _fsyacc_productionToNonTerminalTable  }
let engine lexer lexbuf startState = tables.Interpret(lexer, lexbuf, startState)
let start lexer lexbuf :  int  =
    engine lexer lexbuf 0 :?> _
