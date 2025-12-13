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

        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                if(obj.Name==obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("name","The DisplayOrder cannot exactly match the Name.");
                    return View(obj);
                }
                mAppDb.Categories.Add(obj);
                mAppDb.SaveChanges();

                return RedirectToAction("Index", "Category");
            }
            return View(obj);
        }
    }
}
