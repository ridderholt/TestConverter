module Asserts
    let isAssert (line:string) = 
        let trimmed = line.Trim()
        let isLineAssert = trimmed.StartsWith("Assert")
        isLineAssert

    let fixAsserts (line:string) =
        let str = line.Trim()
        match str with
        | ll when str.StartsWith("Assert.AreEqual") -> line.Replace("Are", "")
        | ll when str.StartsWith("Assert.IsNotNull") -> line.Replace("IsNotNull", "NotNull")
        | ll when str.StartsWith("Assert.IsTrue") -> line.Replace("IsTrue", "True")
        | ll when str.StartsWith("Assert.IsFalse") -> line.Replace("IsFalse", "False")
        | _ -> line


