using System.Linq;
using System.Text;
using System.Web.Mvc;
using DataAccess.Repositories;

namespace Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public string GetSelect()
        {
            var categories = _categoryRepository
                .GetAll()
                .Select(categ => $"<option value='{categ.Id}'>{categ.Name}</option> ");

            var sb = new StringBuilder();
            sb.Append(@"
<select> 
    <option value=''></option>");
            foreach (var category in categories)
                sb.Append(category);
            sb.Append("</select>");
            return sb.ToString();
        }
    }
}