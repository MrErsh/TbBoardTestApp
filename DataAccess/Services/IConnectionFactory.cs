using System.Data;

namespace DataAccess.Services
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}