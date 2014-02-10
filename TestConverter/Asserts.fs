module Asserts
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
        | ll when str.StartsWith("Assert.AreEqual") -> line.Replace("Are", "")
        | ll when str.StartsWith("Assert.IsNotNull") -> line.Replace("IsNotNull", "NotNull")
        | ll when str.StartsWith("Assert.IsTrue") -> line.Replace("IsTrue", "True")
        | ll when str.StartsWith("Assert.IsFalse") -> line.Replace("IsFalse", "False")
        | ll when str.StartsWith("Assert.IsInstanceOfType") -> fixInstance str
        | _ -> line


           

