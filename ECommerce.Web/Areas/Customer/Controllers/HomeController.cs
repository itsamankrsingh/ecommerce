using ECommerce.DataAccess.Interface;
using ECommerce.Models;
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
            List<Product> products = mUnitOfWork.Product.GetAll("Category").ToList();
            return View(products);
        }

        public IActionResult Details(int productId)
        {
            Product product = mUnitOfWork.Product.Get(p=>p.Id==productId,"Category");
            return View(product);
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
