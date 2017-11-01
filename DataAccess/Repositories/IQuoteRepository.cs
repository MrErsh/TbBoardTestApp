using System;
using System.Collections.Generic;
using DataAccess.Model;
using DataAccess.Model.Projections;

namespace DataAccess.Repositories
{
    public interface IQuoteRepository
    {
        void Update(Quote quote);
        void Remove(int rowId);
        void Add(Quote quote);
        IReadOnlyCollection<QuoteFull> GetAllFull(string author, Guid? category);
    }
}