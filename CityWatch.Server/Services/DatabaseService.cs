using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CityWatch.Server.Services
{
    public interface IDatabaseService
    {
        IDbConnection GetContext();
    }
    public class DatabaseService : IDatabaseService
    {
        internal static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private readonly IConfiguration _config;

        private string ConnStringKey = "ConnectionString0";
        private string ConnectionString = "";

        public DatabaseService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IDbConnection GetContext()
        {
            ConnectionString = _config.GetConnectionString(ConnStringKey);
            IDbConnection db = new SqlConnection(ConnectionString);
            if (db.State != ConnectionState.Open)
            {
                db.Open();
            }
            return db;
        }
    }
}
