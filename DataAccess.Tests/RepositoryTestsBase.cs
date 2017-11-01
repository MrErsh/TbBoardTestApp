using System.Data.SqlClient;
using DataAccess.Services;
using Moq;
using NUnit.Framework;

namespace DataAccess.Tests
{
    [TestFixture]
    public class RepositoryTestsBase
    {
        protected IConnectionFactory ConnectionFactory { get; private set; }

        [OneTimeSetUp]
        public void Init()
        {
            var connectionFactorMock = new Mock<IConnectionFactory>();
            connectionFactorMock
                .Setup(factory => factory.Create())
                .Returns(() => new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\me\source\repos\TbBoardTestApp\DataAccess.Tests\Db\TestDb.mdf;Integrated Security=True"));

            ConnectionFactory = connectionFactorMock.Object;
        }
    }
}