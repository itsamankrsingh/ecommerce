using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Interface;
using ECommerce.Identity.Common;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =IdentityRoles.Role_Admin)]
    public class CategoryController : Controller
    {
        //private readonly ApplicationDbContext mAppDb;
        //private readonly ICategoryRepository mCatRepo;
        private readonly IUnitOfWork mUnitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            mUnitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //List<Category> categories = mAppDb.Categories.ToList();
            List<Category> categories = mUnitOfWork.Category.GetAll().ToList();
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
                //mAppDb.Categories.Add(obj);
                //mAppDb.SaveChanges();
                mUnitOfWork.Category.Add(obj);
                mUnitOfWork.Save();
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

            //Category? category = mAppDb.Categories.Find(id);
            Category? category = mUnitOfWork.Category.Get(c => c.Id == id);

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
                //mAppDb.Categories.Update(obj);
                //mAppDb.SaveChanges();
                mUnitOfWork.Category.Update(obj);
                mUnitOfWork.Save();
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

            //Category? category = mAppDb.Categories.Find(id);
            Category? category = mUnitOfWork.Category.Get(c => c.Id == id);
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

            //var category = mAppDb.Categories.Find(id);
            Category? category = mUnitOfWork.Category.Get(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            //mAppDb.Categories.Remove(category);
            //mAppDb.SaveChanges();
            mUnitOfWork.Category.Remove(category);
            mUnitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");


        }
    }
}