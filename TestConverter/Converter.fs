
module Converter
    let parseLine (line:string, className:string) =
        let str = line.Trim() 
        match str with
        | "[TestMethod]" -> "\t\t[Fact]"
        | "[ClassInitialize]" -> ""
        | "[TestInitialize]" -> ""
        | "[TestClass]" -> ""
        | "base.Setup();" -> ""
        | "public override void Setup()" -> ["\t\t";"public "; className; "()"] |> String.concat ""
        | _ -> line

    let containsName (line:string) = 
        let isClassName = line.Trim().StartsWith("public class")         
        isClassName

    let removeClass (line:string) =
        let cleaned = line.Trim().Replace("public class ", "")
        cleaned

    let removeBase (line:string) = 
        let cleaned = line.Trim().Replace(" : ServiceTestBase", "") 
        cleaned
            
    let extractName (line:string) = 
        let name = line.Trim() |> removeClass |> removeBase
        name
               

                

