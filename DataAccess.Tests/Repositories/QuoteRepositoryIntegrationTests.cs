using System;
using System.Data.SqlClient;
using System.Linq;
using DataAccess.Model;
using DataAccess.Repositories;
using NUnit.Framework;

namespace DataAccess.Tests.Repositories
{
    public class QuoteRepositoryIntegrationTests : RepositoryTestsBase
    {
        [SetUp]
        public void SetUp()
        {
            Truncate();
            _repository = new QuoteRepository(ConnectionFactory);

            _repository.Add(new Quote
            {
                Author = "aut1",
                CategoryId = _fstUid,
                Content = "cont1"
            });

            _repository.Add(new Quote
            {
                Author = "aut2",
                CategoryId = _sndUid,
                Content = "cont2"
            });
        }

        [TearDown]
        public void TearDown()
        {
            Truncate();
        }

        private QuoteRepository _repository;

        private readonly Guid _fstUid = new Guid("00000000-0000-0000-0000-000000000001");
        private readonly Guid _sndUid = new Guid("00000000-0000-0000-0000-000000000002");

        private void Truncate()
        {
            using (var connection = ConnectionFactory.Create())
            {
                using (var deleteAllCommand = new SqlCommand("TRUNCATE TABLE Quote", connection as SqlConnection))
                {
                    connection.Open();
                    deleteAllCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        [Test]
        public void Add_should_add_quotes_to_tables()
        {
            var quotes = _repository.GetAllFull(null, null).ToArray();

            Assert.That(quotes, Has.Length.EqualTo(2));
            var fst = quotes.Single(quote => quote.RowId == 1);
            Assert.That(fst.Author, Is.EqualTo("aut1"));
            Assert.That(fst.CategoryId, Is.EqualTo(_fstUid));
            Assert.That(fst.Content, Is.EqualTo("cont1"));
            Assert.That(fst.CreationDate.Date, Is.EqualTo(DateTime.Today));
            var snd = quotes.Single(quote => quote.RowId == 2);
            Assert.That(snd.Author, Is.EqualTo("aut2"));
            Assert.That(snd.CategoryId, Is.EqualTo(_sndUid));
            Assert.That(snd.Content, Is.EqualTo("cont2"));
            Assert.That(snd.CreationDate.Date, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public void GetAll_should_return_quotes_filtered_by_author()
        {
            var thrdUid = new Guid("00000000-0000-0000-0000-000000000003");
            _repository.Add(new Quote
            {
                Author = "aut333345",
                CategoryId = thrdUid,
                Content = "cont3"
            });

            var quotes = _repository.GetAllFull("aut3", null).ToArray();

            Assert.That(quotes, Has.Length.EqualTo(1));
            var quote = quotes[0];
            Assert.That(quote.RowId, Is.EqualTo(3));
            Assert.That(quote.Author, Is.EqualTo("aut333345"));
            Assert.That(quote.CategoryId, Is.EqualTo(thrdUid));
            Assert.That(quote.Content, Is.EqualTo("cont3"));
        }

        [Test]
        public void GetAll_should_return_quotes_filtered_by_categoryId()
        {
            var thrdUid = new Guid("00000000-0000-0000-0000-000000000003");
            _repository.Add(new Quote
            {
                Author = "aut3",
                CategoryId = thrdUid,
                Content = "cont3"
            });

            var quotes = _repository.GetAllFull(null, thrdUid).ToArray();

            Assert.That(quotes, Has.Length.EqualTo(1));
            var quote = quotes[0];
            Assert.That(quote.RowId, Is.EqualTo(3));
            Assert.That(quote.Author, Is.EqualTo("aut3"));
            Assert.That(quote.CategoryId, Is.EqualTo(thrdUid));
            Assert.That(quote.Content, Is.EqualTo("cont3"));
        }

        [Test]
        public void Remove_should_remove_quote_by_rowId()
        {
            _repository.Remove(2);

            var quotes = _repository.GetAllFull(null, null).ToArray();

            Assert.That(quotes, Has.Length.EqualTo(1));
            Assert.That(quotes[0].RowId, Is.EqualTo(1));
        }

        [Test]
        public void Update_should_update_quote()
        {
            var quoteToUpdate = new Quote
            {
                RowId = 2,
                Author = "aut22",
                CategoryId = _fstUid,
                Content = "cont22",
                CreationDate = DateTime.Today.AddYears(11)
            };

            _repository.Update(quoteToUpdate);

            var quotes = _repository.GetAllFull(null, null).ToArray();

            Assert.That(quotes, Has.Length.EqualTo(2));
            var fst = quotes.Single(quote => quote.RowId == 1);
            Assert.That(fst.Author, Is.EqualTo("aut1"));
            Assert.That(fst.CategoryId, Is.EqualTo(_fstUid));
            Assert.That(fst.Content, Is.EqualTo("cont1"));
            Assert.That(fst.CreationDate.Date, Is.EqualTo(DateTime.Today));
            var updated = quotes.Single(quote => quote.RowId == 2);
            Assert.That(updated.Author, Is.EqualTo("aut22"));
            Assert.That(updated.CategoryId, Is.EqualTo(_fstUid));
            Assert.That(updated.Content, Is.EqualTo("cont22"));
            Assert.That(updated.CreationDate.Date, Is.EqualTo(DateTime.Today));
        }
    }
}