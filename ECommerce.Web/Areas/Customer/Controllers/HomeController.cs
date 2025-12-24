using ECommerce.DataAccess.Interface;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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
            List<Product> products = mUnitOfWork.Product.GetAll("Category").ToList();
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
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shoppingCart.ApplicationUserId = userId;

            mUnitOfWork.ShoppingCart.Add(shoppingCart);
            mUnitOfWork.Save();
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
