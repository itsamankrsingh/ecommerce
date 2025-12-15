using ECommerce.DataAccess.Interface;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork mUnitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
                mUnitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> products = mUnitOfWork.Product.GetAll().ToList();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                mUnitOfWork.Product.Add(obj);
                mUnitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index", "Product");
            }
            return View(obj);
        }
    }
}
