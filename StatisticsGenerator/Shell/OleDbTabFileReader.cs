using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    public class OleDbTabFileReader : IDisposable
    {
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataReader reader;

        public OleDbTabFileReader(string file)
        {
            string filename = Path.GetFileName(file);
            string dir = Path.GetDirectoryName(file);

            connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=\"text;HDR=YES;FMT=TabDelimited\";DataSource=" + dir);

            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM " + filename;
            reader = command.ExecuteReader();

            // read the headers, figure out which column is which
        }

        // Use the reader to yield return each row/set of columns

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    command.Dispose();
                    connection.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
