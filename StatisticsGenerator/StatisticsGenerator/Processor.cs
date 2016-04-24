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

            // These are here so that (a) Processor can have a default error/warning system, typically
            // a log file or something, and (b) so that we are guaranteed that the OnError/OnWarning
            // events are never null and therefore always safe to call
            this.OnError += Processor_OnError;
            this.OnWarning += Processor_OnWarning;
            this.OnInformation += Processor_OnInformation;
        }

        private void Processor_OnInformation(string message, params object[] data)
        {
            // Definitely do nothing--just a placeholder
        }

        private void Processor_OnWarning(string message, params object[] data)
        {
            // Do nothing for now; add a System.Diagnostics output stream later
        }

        private void Processor_OnError(string message, params object[] data)
        {
            // Do nothing for now; add a System.Diagnostics output stream later
        }

        public delegate void ReportProc(string message, params object[] data);
        public event ReportProc OnError;
        public event ReportProc OnWarning;
        public event ReportProc OnInformation;

        public List<Configuration> Configurations { get; private set; }

        public void Process(Entry e)
        {
            foreach (var con in Configurations)
            {
                if (con.VariableName == e.VarName)
                {
                    OnInformation("Processing {0}/{1}", e.ScenId, e.VarName);

                    // First time through? Create the List, since it won't exist otherwise
                    if (processedEntries.Keys.Contains(con.ConfigurationName) == false)
                        processedEntries.Add(con.ConfigurationName, new List<Value>());

                    processedEntries[con.ConfigurationName].Add(con.ChoosePeriod(e.Values));
                }
            }
        }

        public Dictionary<string, Value> Calculations()
        {
            var result = new Dictionary<string, Value>();
            foreach (var con in Configurations)
            {
                var values = processedEntries[con.ConfigurationName];
                result[con.ConfigurationName] = con.Calculate(values);
            }
            return result;
        }
    }
}
