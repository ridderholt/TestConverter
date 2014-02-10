module Asserts
    let removeMessage (line:string, max:int) = 
        let str = line.Split([| ", " |], System.StringSplitOptions.RemoveEmptyEntries)
        if str.Length >= max then
            let addAtEnd = [| ");" |]
            let arr = [| 
                            for i=0 to str.Length-2 do
                                if i=0 && max>2 then yield str.[i] + ","
                                else yield str.[i]
                      |]
            let res = Array.append arr addAtEnd |> String.concat ""                
            res
        else
        line                

    let parseAssertTwo (line:string) = 
        let res = removeMessage(line, 2)
        res

    let parseAssertThree (line:string) = 
        let res = removeMessage(line, 3)
        res

    let isAssert (line:string) = 
        let trimmed = line.Trim()
        let isLineAssert = trimmed.StartsWith("Assert")
        isLineAssert

    let fixInstance (line:string) = 
        let stripped1 = line.Replace("Assert.IsInstanceOfType(", "")
        let arr = stripped1.Split(',')
        let res = "\t\t\tAssert.IsType<" + arr.[1].Replace("typeof(", "").Replace(")", "").Replace(";", "").Trim() + ">(" + arr.[0].ToString().Trim() + ");"
        res
 
    let fixAsserts (line:string) =
        let str = line.Trim()
        match str with
        | ll when str.StartsWith("Assert.AreEqual") -> line.Replace("Are", "") |> parseAssertThree
        | ll when str.StartsWith("Assert.AreNotEqual") -> line.Replace("Are", "") |> parseAssertThree
        | ll when str.StartsWith("Assert.IsNotNull") -> line.Replace("IsNotNull", "NotNull") |> parseAssertTwo
        | ll when str.StartsWith("Assert.IsTrue") -> line.Replace("IsTrue", "True") |> parseAssertTwo
        | ll when str.StartsWith("Assert.IsFalse") -> line.Replace("IsFalse", "False") |> parseAssertTwo
        | ll when str.StartsWith("Assert.IsInstanceOfType") -> fixInstance str
        | _ -> line
