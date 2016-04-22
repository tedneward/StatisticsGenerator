using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsGenerator
{
    // If you change this, change it in Processor.cs as well; these should remain
    // in lockstep, and will probably compile-error if they're not
    using Value = Double;


    public enum Calculation
    {
        MinValue,
        MaxValue,
        Average
    }

    public enum PeriodChoice
    {
        FirstValue,
        LastValue,
        MinValue,
        MaxValue
    }

    public class Configuration
    {
        // PeriodChoice operations
        Dictionary<PeriodChoice, Func<List<Value>,Value>> periodChoices = 
            new Dictionary<PeriodChoice, Func<List<Value>, Value>>()
            {
                { PeriodChoice.FirstValue, (vs) => vs[0] },
                { PeriodChoice.LastValue, (vs) => vs[vs.Count - 1] },
                { PeriodChoice.MinValue,  (vs) => vs.Min() },
                { PeriodChoice.MaxValue, (vs) => vs.Max() }
            };

        // Calculation operations
        Dictionary<Calculation, Func<List<Value>, Value>> calculations =
            new Dictionary<Calculation, Func<List<Value>, Value>>()
            {
                { Calculation.MinValue, (vs) => vs.Min() },
                { Calculation.MaxValue, (vs) => vs.Max() },
                { Calculation.Average, (vs) => vs.Average() }
            };


        public Configuration(string name, Calculation calc, PeriodChoice pc)
        {
            this.VariableName = name;
            this.Calculation = calc;
            this.PeriodChoice = pc;
        }

        public string VariableName { get; private set; }
        public Calculation Calculation { get; private set; }
        public PeriodChoice PeriodChoice { get; private set; }

        public double ChoosePeriod(List<Value> periods)
        {
            return periodChoices[PeriodChoice](periods);
        }

        public double Calculate(List<Value> values)
        {
            return calculations[Calculation](values);
        }
    }
}
