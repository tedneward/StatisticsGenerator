using System;
using System.Collections.Generic;
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

        public static void Main(string[] args)
        {
            new Program()
                .ProcessCommandLine(args);
        }
    }
}
