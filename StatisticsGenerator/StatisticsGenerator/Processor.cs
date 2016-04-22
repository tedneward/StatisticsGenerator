using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsGenerator
{
    public class Entry
    {
        public int ScenId { get; set; }
        public string VarName { get; set; }
        public List<float> Values { get; set; }

        public Entry()
        {
            Values = new List<float>();
        }
    }

    public class Processor
    {
        private List<float> processedEntries = new List<float>();

        public Processor(Configuration config)
        {
            this.Configuration = config;
        }

        public Configuration Configuration { get; }

        public void Process(Entry e)
        {

        }
    }
}
