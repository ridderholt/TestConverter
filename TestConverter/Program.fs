open System
open Converter
open Asserts

let convertFile (path:string) = 
    let rowsToAdd = [|"using Xunit;"; "using Assert = Xunit.Assert;"|]
    let rows = System.IO.File.ReadAllLines(path)
    let className = rows |> Array.tryFind containsName |> extractName
    let initIndex = rows |> Array.tryFindIndex findClassInit
    let trans = filterOnIndex(initIndex, rows)
    let transformed = trans |> 
                      Array.filter keepLine |> 
                      Array.map (fun line -> parseLine(line, className)) |>
                      Array.map (fun line -> fixAsserts line)

    let finished = Array.append rowsToAdd transformed
    System.IO.File.WriteAllLines(path, finished)


[<EntryPoint>]
let main argv = 

    let files = System.IO.Directory.GetFiles(@"D:\Workspace\Repos\laget.se\laget.Test\", "*.cs", System.IO.SearchOption.AllDirectories)
    
    //let path = "D:\Workspace\Repos\laget.se\laget.Test\Core\Services\MemeberFeesTests\SaveFeeTests.cs"
    files |> Array.Parallel.iter (fun path -> convertFile path)
    
    0 

