using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StatisticsGenerator;

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

        public static IEnumerable<Dictionary<string, string>> ReadData(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);

            // first line is the header
            string header = reader.ReadLine();
            string[] parts = header.Split('\t');

            // find ScenId index
            int scenIDidx = -1;
            int varNameIdx = -1;
            int valStartIdx = -1;

            for (var count = 0; count < parts.Length; count++)
            {
                switch (parts[count])
                {
                    case "ScenId": scenIDidx = count; break;
                    case "VarName": varNameIdx = count; break;
                    case "Value000": valStartIdx = count; break;
                    default:
                        // Do nothing, it's either a ValueNNN or something we don't care about
                        break;
                }
            }

            // Now, start yielding up lines
            string line = "";
            while ((line = reader.ReadLine()) != null)
            {
                parts = line.Split('\t');

                var lineBits = new Dictionary<string, string>();
                lineBits["ScenId"] = parts[scenIDidx];
                lineBits["VarName"] = parts[varNameIdx];
                for (int p = valStartIdx, valueCount = 0; p < parts.Length; p++, valueCount++)
                {
                    var valueName = String.Format("Value{0:000}", valueCount);
                    var valueValue = parts[p];

                    lineBits[valueName] = valueValue;
                }

                yield return lineBits;
            }
        }

        public static IEnumerable<Entry> ReadEntries(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);

            // first line is the header
            string header = reader.ReadLine();
            string[] parts = header.Split('\t');

            // find ScenId index
            int scenIDidx = -1;
            int varNameIdx = -1;
            int valStartIdx = -1;

            for (var count = 0; count < parts.Length; count++)
            {
                switch (parts[count])
                {
                    case "ScenId": scenIDidx = count; break;
                    case "VarName": varNameIdx = count; break;
                    case "Value000": valStartIdx = count; break;
                    default:
                        // Do nothing, it's either a ValueNNN or something we don't care about
                        break;
                }
            }

            // Now, start yielding up lines
            string line = "";
            while ((line = reader.ReadLine()) != null)
            {
                parts = line.Split('\t');

                var entry = new Entry();
                entry.ScenId = Int32.Parse(parts[scenIDidx]);
                entry.VarName = parts[varNameIdx];

                for (int p = valStartIdx, valueCount = 0; p < parts.Length; p++, valueCount++)
                {
                    var value = Double.Parse(parts[p]);
                    entry.Values.Add(value);
                }

                yield return entry;
            }
        }
    }
}
