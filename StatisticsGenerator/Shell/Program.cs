using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StatisticsGenerator;

namespace Shell
{
    // Public fields?!? Yup; there's nobody I'm trying to encapsulate this code from, since this is
    // "top-of-the-stack" code. The public fields make this class much easier to test without having
    // to get into weird mocking/stubs situations.
    //
    public class Program
    {
        public string configFile = "";
        public List<string> tempFiles = new List<string>();
        public Processor processor = new Processor();

        public Program ProcessCommandLine(string[] args)
        {
            if (args.Count() < 2)
            {
                Console.WriteLine("USAGE: Shell <configurationFile> <tempfile1> <tempfile2> ... <tempfileN>");
                throw new InvalidOperationException("Improper command-line arguments");
            }

            configFile = args[0];
            for (var i = 1; i < args.Count(); i++)
                tempFiles.Add(args[i]);

            return this;
        }

        public Program ProcessConfigFile()
        {
            StreamReader reader = new StreamReader(configFile);
            string line = "";
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('\t');

                var varName = parts[0];
                var statCalc = (parts[1] == "MinValue") ? Calculation.MinValue :
                    (parts[1] == "MaxValue") ? Calculation.MaxValue :
                    Calculation.Average;
                var periodChoice = (parts[2] == "FirstValue") ? PeriodChoice.FirstValue :
                    (parts[2] == "LastValue") ? PeriodChoice.LastValue :
                    (parts[2] == "MinValue") ? PeriodChoice.MinValue :
                    PeriodChoice.MaxValue;

                processor.Configurations.Add(new Configuration(varName, statCalc, periodChoice));
            }

            return this;
        }

        public Program ProcessTempFiles()
        {
            foreach (var file in tempFiles)
            {
                foreach (var entry in TabFileReader.ReadEntries(new FileStream(file, FileMode.Open)))
                {
                    processor.Process(entry);
                }
            }

            return this;
        }

        public Program PrintResults()
        {
            var results = processor.Calculations();
            foreach (var key in results.Keys)
            {
                Console.WriteLine("{0}\t{1}", key, results[key]);
            }

            return this;
        }

        public Program WriteResults()
        {
            using (var outputFile = new StreamWriter("results.txt"))
            {
                var results = processor.Calculations();
                foreach (var key in results.Keys)
                {
                    outputFile.WriteLine("{0}\t{1}", key, results[key]);
                }
            }

            return this;
        }

        public static void Main(string[] args)
        {
            new Program()
                .ProcessCommandLine(args)
                .ProcessConfigFile()
                .ProcessTempFiles()
                .PrintResults();
        }
    }
}
