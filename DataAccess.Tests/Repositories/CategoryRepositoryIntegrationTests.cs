using System;
using System.Linq;
using DataAccess.Repositories;
using NUnit.Framework;

namespace DataAccess.Tests.Repositories
{
    // Предполагается что таблица Category не изменяется во время тестов.
    public class CategoryRepositoryIntegrationTests : RepositoryTestsBase
    {
        [Test]
        public void GetAll_shoud_return_all_categories()
        {
            var repository = new CategoryRepository(ConnectionFactory);

            var categories = repository.GetAll().ToArray();

            Assert.That(categories, Has.Length.EqualTo(3));
            var fst = categories.Single(categ => categ.Id == new Guid("00000000-0000-0000-0000-000000000001"));
            Assert.That(fst.Name, Is.EqualTo("Первая"));
            var snd = categories.Single(categ => categ.Id == new Guid("00000000-0000-0000-0000-000000000002"));
            Assert.That(snd.Name, Is.EqualTo("Вторая"));
            var thrd = categories.Single(categ => categ.Id == new Guid("00000000-0000-0000-0000-000000000003"));
            Assert.That(thrd.Name, Is.EqualTo("Третяя"));
        }
    }
}