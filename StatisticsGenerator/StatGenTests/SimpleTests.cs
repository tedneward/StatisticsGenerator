using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using StatisticsGenerator;

namespace StatGenTests
{
    /*
     * I use these as "scratchpad" tests, as a way of fleshing out my thinking.
     * Were I in a REPL-based language, I could do this using the REPL, but I
     * actually prefer to use tests--I can keep the tests over time, and if I'm
     * good about creating new test methods for each idea, then I can see how my
     * ideas evolve over time.
     */
    [TestClass]
    public class SimpleTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var tested = Simple.TestMe();

            Assert.AreEqual("I have been tested, and found capable!", tested);
        }

        [TestMethod]
        public void PeriodChoiceFirstValueOperation()
        {
            var values = new List<double>();
            values.Add(0.00);
            values.Add(0.04);
            values.Add(0.04);
            values.Add(0.04);
            values.Add(0.03);

            Func<List<double>, double> firstValue = (vs) => vs[0];

            var res = firstValue(values);
            Assert.AreEqual(0.00, res);
        }

        [TestMethod]
        public void PeriodChoiceTable()
        {
            var periodChoices = new Dictionary<PeriodChoice, Func<List<double>, double>>()
            {
                { PeriodChoice.FirstValue, (vs) => vs[0] },
                { PeriodChoice.LastValue, (vs) => vs[vs.Count - 1] },
                { PeriodChoice.MinValue,  (vs) => vs.Min() },
                { PeriodChoice.MaxValue, (vs) => vs.Max() }
            };

            var values = new List<double>();
            values.Add(0.03);
            values.Add(0.01);
            values.Add(0.04);
            values.Add(0.00);
            values.Add(0.02);

            var first = periodChoices[PeriodChoice.FirstValue](values);
            var last = periodChoices[PeriodChoice.LastValue](values);
            var min = periodChoices[PeriodChoice.MinValue](values);
            var max = periodChoices[PeriodChoice.MaxValue](values);

            Assert.AreEqual(0.03, first);
            Assert.AreEqual(0.02, last);
            Assert.AreEqual(0.00, min);
            Assert.AreEqual(0.04, max);
        }
    }
}
