﻿module Converter
    let parseLine (line:string, className:string) =
        let str = line.Trim() 
        match str with
        | "[TestMethod]" -> "\t\t[Fact]"
        | "public override void Setup()" -> ["\t\t";"public "; className; "()"] |> String.concat ""
        | _ -> line

    let keepLine (line:string) = 
        let str = line.Trim()
        match str with
        | "[ClassInitialize]" -> false 
        | "[TestInitialize]" -> false 
        | "[TestClass]" -> false 
        | "base.Setup();" -> false
        | _ -> true


    let containsName (line:string) = 
        let isClassName = line.Trim().StartsWith("public class")         
        isClassName

    let removeClass (line:string) =
        let cleaned = line.Trim().Replace("public class ", "")
        cleaned

    let removeBase (line:string) = 
        let cleaned = line.Trim().Replace(" : ServiceTestBase", "") 
        cleaned
            
    let extractName (line:string option) = 
        if line.IsNone then
            ""
        else
            let name = line.Value.Trim() |> removeClass |> removeBase
            name
               
    let findClassInit (line:string) = 
        let str = line.Trim()
        let isInit = str.Equals("public static void ClassTestInit(TestContext context)")
        isInit

    let filterOnIndex (index:int option, arr) = 
        if index.IsNone then
            arr
        else
            let nArr = [| 
                            for i=0 to Array.length arr - 1 do
                                if i < index.Value || i > (index.Value + 4) then yield arr.[i]
                       |]
            nArr

