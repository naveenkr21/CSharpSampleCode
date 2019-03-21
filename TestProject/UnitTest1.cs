using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocumentProcessor;
using System.Collections.Generic;
using System.IO;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_IsValidNamingConvention()
        {
            DirectoryInfo d = new DirectoryInfo(utility.cmspath);
            List<string> ls = new List<string>()
            {
                ".tif", ".txt"
            };
            using (SimpleDocProcessor dp = new SimpleDocProcessor(d, ls))
            {
                var result = dp.IsValidNamingConvention("doc_t.txt");
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void Test_IsValidExtension()
        {
            DirectoryInfo d = new DirectoryInfo(utility.cmspath);
            List<string> ls = new List<string>()
            {
                ".tif", ".txt"
            };
            using (SimpleDocProcessor dp = new SimpleDocProcessor(d, ls))
            {
                FileInfo f = new FileInfo(Path.Combine(utility.cmspath, "doc_t.txt"));
                var result = dp.IsValidExtention(f, ls);
                Assert.AreEqual(true, result);
            }
        }

    }
}
