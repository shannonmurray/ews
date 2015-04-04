using System;
using System.Data.SqlClient;

namespace HttpServices
{
    public class DbConn : IDisposable
    {
        public SqlConnection Conn { get; private set; }

        public DbConn(String connection)
        {
            Conn = new SqlConnection(connection);
            Conn.Open();
        }

        public void Dispose()
        {
            if (Conn != null)
            {
                Conn.Close();
                Conn.Dispose();
                Conn = null;
            }
        }
    }
}
