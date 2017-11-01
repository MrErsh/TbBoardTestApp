using System.Collections.Generic;
using DataAccess.Model;

namespace DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        IReadOnlyCollection<Category> GetAll();
    }
}