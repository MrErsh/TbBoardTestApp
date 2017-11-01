using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataAccess.Model;
using DataAccess.Model.Projections;
using DataAccess.Services;

namespace DataAccess.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        #region Queries
        private const string RemoveQuery = "DELETE FROM Quote WHERE RowId = @RowId";

        private const string AddQuery = @"
INSERT INTO Quote(Author, Content, CategoryId)
VALUES(@Author, @Content, @CategoryId)";

        private readonly string GetAllQuery = @"
SELECT q.*, c.[Name] CategoryName
FROM Quote q
JOIN Category c ON c.Id = q.CategoryId
WHERE (@CategoryId IS NULL OR q.CategoryId = @CategoryId)
    AND (@Author IS NULL OR q.Author LIKE @Author + '%')";

        private const string UpdateQuery = @"
UPDATE Quote
SET Author = @Author,
	Content = @Content,
	CategoryId = @CategoryId
WHERE RowId = @RowId
";

        #endregion 

        private readonly IConnectionFactory _connectionFactory;

        public QuoteRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Update(Quote quote)
        {
            using (var connection = _connectionFactory.Create())
            {
                connection.Execute(UpdateQuery, quote);
            }
        }

        public void Remove(int rowId)
        {
            using (var connection = _connectionFactory.Create())
            {
                connection.Execute(RemoveQuery, new {Rowid = rowId});
            }
        }

        public void Add(Quote quote)
        {
            using (var connection = _connectionFactory.Create())
            {
                connection.Execute(AddQuery, quote);
            }
        }

        public IReadOnlyCollection<QuoteFull> GetAllFull(string author, Guid? category)
        {
            using (var connection = _connectionFactory.Create())
            {
                return connection
                    .Query<QuoteFull>(GetAllQuery, new
                    {
                        Author = author,
                        CategoryId = category
                    })
                    .ToArray();
            }
        }
    }
}