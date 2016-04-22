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
        private List<Value> processedEntries = new List<Value>();

        public Processor()
        {
            this.Configurations = new List<Configuration>();
        }

        public List<Configuration> Configurations { get; private set; }

        public void Process(Entry e)
        {

        }
    }
}
