using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    public class TabFileReader
    {
        public static IEnumerable<string> ReadLines(string fullPath)
        {
            return ReadLines(new FileStream(fullPath, FileMode.Open));
        }

        public static IEnumerable<string> ReadLines(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string line = "";
            while ((line = reader.ReadLine()) != null)
                yield return line;

            reader.Close();
        }
    }
}
