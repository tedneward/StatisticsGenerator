using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using StatisticsGenerator;

namespace StatGenTests
{
    [TestClass]
    public class SimpleTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var tested = StatisticsGenerator.Simple.TestMe();

            Assert.AreEqual("I have been tested, and found capable!", tested);
        }
    }
}
