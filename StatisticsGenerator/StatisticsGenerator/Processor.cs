using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsGenerator
{
    // If you change this, change it in Configuration.cs as well; these should remain
    // in lockstep, and will probably compile-error if they're not
    using Value = Double;

    public class Entry
    {
        public int ScenId { get; set; }
        public string VarName { get; set; }
        public List<Value> Values { get; set; }

        public Entry()
        {
            Values = new List<double>();
        }
    }

    public class Processor
    {
        private Dictionary<string, List<Value>> processedEntries = new Dictionary<string, List<Value>>();

        public Processor()
        {
            this.Configurations = new List<Configuration>();
        }

        public List<Configuration> Configurations { get; private set; }

        public void Process(Entry e)
        {
            foreach (var con in Configurations)
            {
                if (con.VariableName == e.VarName)
                {
                    // First time through? Create the List, since it won't exist otherwise
                    if (processedEntries.Keys.Contains(con.VariableName) == false)
                        processedEntries.Add(con.VariableName, new List<Value>());

                    processedEntries[con.VariableName].Add(con.ChoosePeriod(e.Values));
                }
            }
        }

        public Dictionary<string, Value> Calculations()
        {
            var result = new Dictionary<string, Value>();
            foreach (var con in Configurations)
            {
                var values = processedEntries[con.VariableName];
                // Is it possible that there will be configurations that don't have actual varnames in the data?
                // This will exception-out if it is.
                result[con.VariableName] = con.Calculate(values);
            }
            return result;
        }
    }
}
