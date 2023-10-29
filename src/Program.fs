// For more information see https://aka.ms/fsharp-console-apps
// Learn more about F# at http://fsharp.net

open System.IO
open FSharp.Text.Lexing

let parseString text =
    let lexbuf = LexBuffer<char>.FromString text

    let ast = Parser.start Lexer.tokenstream lexbuf

    ()

//printfn "countFromParser: result = %d, expected %d" countFromParser expectedCount

let parseFile (fileName: string) =
    use textReader = new System.IO.StreamReader(fileName)
    let lexbuf = LexBuffer<char>.FromTextReader textReader
    printfn "%A" <| lexbuf.ToString()

    let ast = Parser.start Lexer.tokenstream lexbuf
    printfn "%A" <| ast.ToString()
    ()

//printfn "countFromParser: result = %d, expected %d" countFromParser expectedCount

//testLexerAndParserFromString "hello" 1
//testLexerAndParserFromString "hello hello" 2

let testFile = Path.Combine(__SOURCE_DIRECTORY__, "test.txt")
//File.WriteAllText(testFile, "hello hello")
parseFile testFile

printfn "Press any key to continue..."
System.Console.ReadLine() |> ignore
