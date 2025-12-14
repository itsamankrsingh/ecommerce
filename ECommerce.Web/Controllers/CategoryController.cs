using ECommerce.DataAccess.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext mAppDb;
        public CategoryController(ApplicationDbContext _appDb)
        {
            mAppDb = _appDb;
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
                if (obj.Name == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
                    return View(obj);
                }
                mAppDb.Categories.Add(obj);
                mAppDb.SaveChanges();
                TempData["success"] = "Category created successfully";  
                return RedirectToAction("Index", "Category");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? category = mAppDb.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                mAppDb.Categories.Update(obj);
                mAppDb.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? category = mAppDb.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = mAppDb.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            mAppDb.Categories.Remove(category);
            mAppDb.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");


        }
    }
}