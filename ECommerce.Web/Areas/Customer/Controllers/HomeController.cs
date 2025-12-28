using ECommerce.DataAccess.Interface;
using ECommerce.Models;
using ECommerce.Utility.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECommerce.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> mlogger;
        private readonly IUnitOfWork mUnitOfWork;
        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            mlogger = logger;
            mUnitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> products = mUnitOfWork.Product.GetAll(null,"Category").ToList();
            return View(products);
        }

        public IActionResult Details(int productId)
        {
            ShoppingCart shoppingCart = new()
            {
                Product = mUnitOfWork.Product.Get(p => p.Id == productId, "Category"),
                Count = 1,
                ProductId = productId
            };
            
            return View(shoppingCart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var userId = CommonHelper.GetUserId(User);
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = mUnitOfWork.ShoppingCart.Get(c=>c.ApplicationUserId==userId &&c.ProductId==shoppingCart.ProductId);

            if (cartFromDb != null)
            {
                cartFromDb.Count += shoppingCart.Count;
                mUnitOfWork.ShoppingCart.Update(cartFromDb);
            }
            else
            {
                mUnitOfWork.ShoppingCart.Add(shoppingCart);
            }
            mUnitOfWork.Save();
            TempData["success"] = "Cart updated successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
