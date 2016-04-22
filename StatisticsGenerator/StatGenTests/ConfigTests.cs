using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using StatisticsGenerator;

namespace StatGenTests
{
    [TestClass]
    public class ConfigurationTests
    {
        List<double> values1 = new List<double>() { 0.2, 0.4, 0.0, 0.3, 0.1 };
        List<double> values2 = new List<double>() { 0, 165215335.38, 130922548.81, 107196660.00, 92462698.42, 84655947.13 };

        [TestMethod]
        public void TestFirstValues()
        {
            Configuration con = new Configuration("", Calculation.Average, PeriodChoice.FirstValue);
                // name and calculation unused in this test

            var result = con.ChoosePeriod(values1);
            Assert.AreEqual(0.2, result);

            result = con.ChoosePeriod(values2);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestLastValues()
        {
            Configuration con = new Configuration("", Calculation.Average, PeriodChoice.LastValue);
                // name and calculation unused in this test

            var result = con.ChoosePeriod(values1);
            Assert.AreEqual(0.1, result);

            result = con.ChoosePeriod(values2);
            Assert.AreEqual(84655947.13, result);
        }

        [TestMethod]
        public void TestMinValues()
        {
            Configuration con = new Configuration("", Calculation.Average, PeriodChoice.MinValue);
                // name and calculation unused in this test

            var result = con.ChoosePeriod(values1);
            Assert.AreEqual(0.0, result);

            result = con.ChoosePeriod(values2);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestMaxValues()
        {
            Configuration con = new Configuration("", Calculation.Average, PeriodChoice.MaxValue);
                // name and calculation unused in this test

            var result = con.ChoosePeriod(values1);
            Assert.AreEqual(0.4, result);

            result = con.ChoosePeriod(values2);
            Assert.AreEqual(165215335.38, result);
        }
    }
}
