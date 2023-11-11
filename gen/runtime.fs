open System.Reflection
open System
open System.IO


let filename = "./Gen.dll"
let assembly = Assembly.GetExecutingAssembly()
for r in assembly.GetManifestResourceNames() do
    printfn "%A" r

let resourceName = "Main.Gen.dll"
let result = 
    use f = assembly.GetManifestResourceStream(resourceName)
    use outputFileStream = File.Create(filename)
    f.CopyTo(outputFileStream)
    
let entrypoint f =
    printfn "Loading asm"
    let  assm = Assembly.LoadFrom(f)
    printfn "Finding Program type"
    let prog = assm.ExportedTypes |> Seq.head // we only have one root type (program type)
    printfn "Searching for main method"
    let mutable res = new obj()
    for m in prog.GetMethods() do
        if m.Name = "Main" then // find main
            printfn "Found main"
            //Console.WriteLine(m.Name)
            //Seq.iter (printfn "%A") <| m.GetParameters() 
            //Console.WriteLine( m.ReturnParameter)
            res <- m.Invoke(Activator.CreateInstance(prog), [||]); // here we will eventually pass through the cmdln args
            Console.WriteLine(res)

    // if File.Exists filename then File.Delete filename
    Convert.ToInt32(res)

[<EntryPoint>]
let main args =
    entrypoint filename
    //entrypoint_bin result
