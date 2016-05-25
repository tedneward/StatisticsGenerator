using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Shell;
using System.Text;

namespace ShellTests
{
    [TestClass]
    public class TabReaderTests
    {
        Stream stream;

        public Stream PrepStream()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ScenId\tVarName\tValue000\tValue001\tValue002\tValue003\tValue004\tValue005");
            sb.AppendLine("1\tAvePolLoanYield\t0.00\t0.04\t0.4\t0.04\t0.04\t0.03");
            sb.AppendLine("1\tCashPrem\t0\t165215335.38\t130922548.81\t107196660.00\t92462698.42\t84655947.13");

            MemoryStream stream = new MemoryStream();
            byte[] buffer = Encoding.ASCII.GetBytes(sb.ToString());
            stream.Write(buffer, 0, buffer.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public Stream NonsensicalStream()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("1\t2\t3");
            sb.AppendLine("11\t22\t33");

            MemoryStream stream = new MemoryStream();
            byte[] buffer = Encoding.ASCII.GetBytes(sb.ToString());
            stream.Write(buffer, 0, buffer.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        [TestMethod]
        public void ReadLinesFromInMemoryStream()
        {
            var stream = NonsensicalStream();

            int counter = 0;
            foreach (var line in TabFileReader.ReadLines(stream))
            {
                counter++;
            }

            Assert.AreEqual(3, counter); // 3 lines total
        }

        [TestMethod]
        public void ReadRealLinesFromInMemoryStream()
        {
            var stream = PrepStream();

            int counter = 0;
            foreach (var line in TabFileReader.ReadLines(stream))
            {
                counter++;
            }

            Assert.AreEqual(3, counter); // 3 lines total
        }

        [TestMethod]
        public void ReadLineBitsFromStream()
        {
            var stream = PrepStream();

            int counter = 0;
            foreach (var dict in TabFileReader.ReadData(stream))
            {
                counter++;

                Assert.IsTrue(dict.ContainsKey("ScenId"));
                Assert.IsTrue(dict["ScenId"] == "1");
                Assert.IsTrue(dict.ContainsKey("VarName"));
                Assert.IsTrue(dict.ContainsKey("Value000"));
                Assert.IsTrue(dict.ContainsKey("Value001"));
                Assert.IsTrue(dict.ContainsKey("Value002"));
                Assert.IsTrue(dict.ContainsKey("Value003"));
                Assert.IsTrue(dict.ContainsKey("Value004"));
                Assert.IsTrue(dict.ContainsKey("Value005"));
                Assert.IsFalse(dict.ContainsKey("Value006"));
            }

            Assert.AreEqual(2, counter); // header line should be stripped out
        }

        [TestMethod]
        public void ReadEntriesFromStream()
        {
            var stream = PrepStream();

            int counter = 0;
            foreach (var entry in TabFileReader.ReadEntries(stream))
            {
                counter++;

                Assert.AreEqual(entry.ScenId, 1);
                Assert.AreNotEqual(entry.VarName, "");
                Assert.AreEqual(entry.Values.Count, 6);
            }

            Assert.AreEqual(2, counter); // header line should be stripped out
        }
    }
}
