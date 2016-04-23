using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shell;

namespace StatGenTests
{
    [TestClass]
    public class ShellTests
    {
        [TestMethod]
        public void CmdLineArgumentsGood()
        {
            var program = new Program()
                .ProcessCommandLine(new string[] { "config.txt", "datafile1.txt", "datafile2.txt" });

            Assert.AreEqual("config.txt", program.configFile);
            Assert.AreEqual(2, program.tempFiles.Count);
            Assert.AreEqual("datafile1.txt", program.tempFiles[0]);
            Assert.AreEqual("datafile2.txt", program.tempFiles[1]);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CmdLineArgumentsBad()
        {
            new Program()
                .ProcessCommandLine(new string[] { "config.txt" });
                // Should throw immediately
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void CmdLineArgumentsMissing()
        {
            new Program()
                .ProcessCommandLine(new string[] { });
            // Should throw immediately
        }
    }
}
