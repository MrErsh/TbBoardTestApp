using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Services
{
    public class ConnectionFactory : IConnectionFactory
    {
        public IDbConnection Create() =>
            new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
    }
}