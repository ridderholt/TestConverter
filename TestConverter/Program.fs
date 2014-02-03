open System
open Converter



[<EntryPoint>]
let main argv = 
    let path = "<REPLACE WITH PATH TO TEST FILE>"

    let rows = System.IO.File.ReadAllLines(path)
    let className = rows |> Array.find containsName |> extractName
    let transformed = rows |> Array.map (fun line -> parseLine(line, className))

    System.IO.File.WriteAllLines(path, transformed)

    0 

