using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataAccess.Model;
using DataAccess.Services;

namespace DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private const string GetAllQuery = "SELECT Id, Name FROM Category";

        private readonly IConnectionFactory _connectionFactory;

        public CategoryRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IReadOnlyCollection<Category> GetAll()
        {
            using (var connection = _connectionFactory.Create())
            {
                return connection
                    .Query<Category>(GetAllQuery)
                    .ToArray();
            }
        }
    }
}