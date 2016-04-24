using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using StatisticsGenerator;

namespace StatGenTests
{
    [TestClass]
    public class ProcessingTests
    {
        public TestContext TestContext { get; set; }

        List<Entry> entries = new List<Entry>()
        {
            new Entry() { ScenId = 1, VarName = "AvePolLoanYield", Values = new List<double>() { 0.00, 0.04, 0.04, 0.04, 0.04, 0.03 } },
            new Entry() { ScenId = 1, VarName = "CashPrem", Values = new List<double>() { 0, 165215335.38, 130922548.81, 107196660.00, 92462698.42, 84655947.13 } },
            new Entry() { ScenId = 1, VarName = "ResvAssumed", Values = new List<double>() { -27923645.44, -28437248.89, -29893491.30, -31761676.09, -34092668.16, -36815307.05 } },
            new Entry() { ScenId = 2, VarName = "AvePolLoanYield", Values = new List<double>() { 0.00, 0.04, 0.04, 0.04, 0.04, 0.03 } },
            new Entry() { ScenId = 2, VarName = "CashPrem", Values = new List<double>() { 0, 0, 130922548.81, 107196444.36, 92462698.42, 84655914.86 } },
            new Entry() { ScenId = 2, VarName = "ResvAssumed", Values = new List<double>() { -27923645.44, -28437248.89, -29893531.02, -31762115.98, -34094542.44, -36821010.24 } },
            new Entry() { ScenId = 3, VarName = "AvePolLoanYield", Values = new List<double>() { 0.00, 0.04, 0.04, 0.04, 0.04, 0.03 } },
            new Entry() { ScenId = 3, VarName = "CashPrem", Values = new List<double>() { 0, 0, 0, 107196660.00, 92462698.42, 84655947.13 } },
            new Entry() { ScenId = 3, VarName = "ResvAssumed", Values = new List<double>() { -27923645.44, -28437248.89, -29893482.02, -31761477.70, -34091316.73, -36811494.23 } }
        };

        [TestMethod]
        public void VerifyCashPremExampleSteps()
        {
            Configuration con = new Configuration("CashPrem", Calculation.Average, PeriodChoice.MaxValue);

            // Verify we get the right period, based on the example in the spec
            var results = new List<double>();

            var result = con.ChoosePeriod(entries[1].Values);
            Assert.AreEqual(165215335.38, result, 0.1);
            results.Add(result);

            result = con.ChoosePeriod(entries[4].Values);
            Assert.AreEqual(130922548.81, result, 0.1);
            results.Add(result);

            result = con.ChoosePeriod(entries[7].Values);
            Assert.AreEqual(107196660.00, result, 0.1);
            results.Add(result);

            // Verify we get the right calculation, based on the example in the spec
            result = con.Calculate(results);
            Assert.AreEqual(134444848.1, result, 0.1);
        }

        [TestMethod]
        public void CashPremExampleTest()
        {
            Processor proc = new Processor();
            proc.OnInformation += (message, data) => { TestContext.WriteLine(message, data); };

            proc.Configurations.Add(new Configuration("CashPrem", Calculation.Average, PeriodChoice.MaxValue));

            foreach (var e in entries)
                proc.Process(e);

            var result = proc.Calculations();
            Assert.AreEqual(134444848.1, result["CashPrem-Average-MaxValue"], 0.1);
        }

        [TestMethod]
        public void MultipleCashPremExampleTest()
        {
            Processor proc = new Processor();

            proc.Configurations.Add(new Configuration("CashPrem", Calculation.Average, PeriodChoice.MaxValue));
            proc.Configurations.Add(new Configuration("CashPrem", Calculation.Average, PeriodChoice.MinValue));
            proc.Configurations.Add(new Configuration("CashPrem", Calculation.Average, PeriodChoice.FirstValue));

            foreach (var e in entries)
                proc.Process(e);

            var result = proc.Calculations();
            Assert.AreEqual(134444848.1, result["CashPrem-Average-MaxValue"], 0.1);
            Assert.AreEqual(0, result["CashPrem-Average-MinValue"], 0.1);
            Assert.AreEqual(0, result["CashPrem-Average-FirstValue"], 0.1);
        }

        [TestMethod]
        public void DupedCashPremExampleTest()
        {
            Processor proc = new Processor();

            proc.Configurations.Add(new Configuration("CashPrem", Calculation.Average, PeriodChoice.MaxValue));
            proc.Configurations.Add(new Configuration("CashPrem", Calculation.Average, PeriodChoice.MaxValue));
            proc.Configurations.Add(new Configuration("CashPrem", Calculation.Average, PeriodChoice.MaxValue));

            foreach (var e in entries)
                proc.Process(e);

            var result = proc.Calculations();
            Assert.AreEqual(134444848.1, result["CashPrem-Average-MaxValue"], 0.1);
        }
    }
}
