using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsGenerator
{
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
        Dictionary<PeriodChoice, Func<List<double>,double>> periodChoices = 
            new Dictionary<PeriodChoice, Func<List<double>, double>>()
            {
                { PeriodChoice.FirstValue, (vs) => vs[0] },
                { PeriodChoice.LastValue, (vs) => vs[vs.Count - 1] },
                { PeriodChoice.MinValue,  (vs) => vs.Min() },
                { PeriodChoice.MaxValue, (vs) => vs.Max() }
            };

        // Calculation operations
        Dictionary<Calculation, Func<List<double>, double>> calculations =
            new Dictionary<Calculation, Func<List<double>, double>>()
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

        public double ChoosePeriod(List<double> periods)
        {
            return periodChoices[PeriodChoice](periods);
        }

        public double Calculate(List<double> values)
        {
            return calculations[Calculation](values);
        }
    }
}
