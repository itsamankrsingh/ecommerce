using ECommerce.DataAccess.Interface;
using ECommerce.DataAccess.Repository;
using ECommerce.Models;
using ECommerce.Utility.Helper;
using ECommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        public CartViewModel mCartViewModel;
        private readonly IUnitOfWork mUnitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            mUnitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            mCartViewModel = new CartViewModel()
            {
                ShoppingCarts = mUnitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == userId, "Product").OrderBy(u => u.ProductId),
            };
            mCartViewModel.OrderTotal = 0;
            foreach (var cartItem in mCartViewModel.ShoppingCarts)
            {
                mCartViewModel.OrderTotal += CommonHelper.CalculateCartTotal(cartItem, mCartViewModel.OrderTotal);
            }

            return View(mCartViewModel);
        }

        public IActionResult Plus(int cartId)
        {
            var cartFromDb = mUnitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            cartFromDb.Count += 1;
            mUnitOfWork.ShoppingCart.Update(cartFromDb);
            mUnitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cartFromDb = mUnitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            if (cartFromDb.Count <= 1)
            {
                mUnitOfWork.ShoppingCart.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                mUnitOfWork.ShoppingCart.Update(cartFromDb);
            }
            mUnitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Remove(int cartId)
        {
            var cartFromDb = mUnitOfWork.ShoppingCart.Get(u => u.Id == cartId);

            if (cartFromDb == null)
            {
                return Json(new { success = false, message = "Item not found" });
            }

            mUnitOfWork.ShoppingCart.Remove(cartFromDb);

            mUnitOfWork.Save();

            // Recalculate total
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCarts = mUnitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == userId, "Product").OrderBy(u => u.ProductId);
            double newTotal = 0; 
            foreach (var cartItem in shoppingCarts)
            {
                newTotal += CommonHelper.CalculateCartTotal(cartItem, newTotal);
            }

            return Json(new { success = true, total = newTotal });
        }

        public IActionResult Summary(int cartId)
        {
            return View();
        }

        #region API Calls
        #endregion
    }
}