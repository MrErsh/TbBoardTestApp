using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DataAccess.Model;
using DataAccess.Repositories;
using Newtonsoft.Json;

namespace Web.Controllers
{
    public class QuoteController : Controller
    {
        private readonly IQuoteRepository _quotes;
        private readonly ICategoryRepository _categoryRepository;

        public QuoteController(IQuoteRepository quotes, ICategoryRepository categoryRepository)
        {
            _quotes = quotes;
            _categoryRepository = categoryRepository;
        }

        public ActionResult Index()
        {
            var categories = _categoryRepository
                .GetAll()
                .Select(categ => new SelectListItem {Text = categ.Name, Value = categ.Id.ToString()})
                .ToArray();

            ViewBag.Categories = new[]
                {
                    new SelectListItem
                    {
                        Text = string.Empty,
                        Value = null
                    }
                }
                .Union(categories);
            return View();
        }

        public ContentResult GetAll(string author, Guid? category)
        {
            var quotes = _quotes.GetAllFull(author, category);
            return new ContentResult {Content = JsonConvert.SerializeObject(quotes)};
        }

        [HttpPost]
        public void Edit(Quote quote)
        {
            _quotes.Update(quote);
        }

        [HttpPost]
        public void Create(Quote quote)
        {
            _quotes.Add(quote);
        }

        [HttpPost]
        public void Delete(int id)
        {
            _quotes.Remove(id);
        }
    }
}