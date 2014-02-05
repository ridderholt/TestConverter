open System
open Converter



[<EntryPoint>]
let main argv = 
    let path = "D:\Workspace\Repos\laget.se\laget.Test\Core\Services\MemeberFeesTests\SaveFeeTests.cs"

    let rows = System.IO.File.ReadAllLines(path)
    let className = rows |> Array.find containsName |> extractName
    let initIndex = rows |> Array.findIndex findClassInit
    let trans = filterOnIndex(initIndex, rows)
    let transformed = trans |> Array.filter keepLine |> Array.map (fun line -> parseLine(line, className))

    System.IO.File.WriteAllLines(path, transformed)

    0 

