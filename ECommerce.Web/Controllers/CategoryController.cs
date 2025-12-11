using ECommerce.Web.Data;
using ECommerce.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext mAppDb;
        public CategoryController(ApplicationDbContext _appDb)
        {
                mAppDb=_appDb;
        }
        public IActionResult Index()
        {
            List<Category> categories = mAppDb.Categories.ToList();
            return View(categories);
        }
    }
}
