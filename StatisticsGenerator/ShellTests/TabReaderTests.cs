using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Shell;
using System.Text;

namespace ShellTests
{
    [TestClass]
    public class TabReaderTests
    {
        [TestMethod]
        public void ReadLinesFromInMemoryStream()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("One\tTwo\tThree");
            sb.AppendLine("1\t2\t3");
            sb.AppendLine("11\t22\t33");

            MemoryStream stream = new MemoryStream();
            byte[] buffer = Encoding.ASCII.GetBytes(sb.ToString());
            stream.Write(buffer, 0, buffer.Length);
            stream.Seek(0, SeekOrigin.Begin);

            int counter = 0;
            foreach (var line in TabFileReader.ReadLines(stream))
            {
                counter++;
            }

            Assert.AreEqual(3, counter); // 3 lines total
        }
    }
}
