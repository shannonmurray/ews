
using System.Data.SqlClient;
using Funq;
using ServiceStack.WebHost.Endpoints;

namespace HttpServices
{
    public class AppHost : AppHostBase
    {
        private readonly string _connectionString;

        public AppHost(string connStr) : base("EWS Server", typeof(ProbeService).Assembly)
        {
            _connectionString = connStr;
        }

        public override void Configure(Container container)
        {
            container.Register(c => new DbConn(_connectionString));
        }
    }
}
