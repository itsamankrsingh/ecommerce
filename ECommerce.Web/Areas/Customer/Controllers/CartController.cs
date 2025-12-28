using ECommerce.DataAccess.Interface;
using ECommerce.Identity.Interface;
using ECommerce.Utility.Helper;
using ECommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        public CartViewModel mCartViewModel;
        private readonly IUnitOfWork mUnitOfWork;
        private readonly IApplicationUserRepository mAppUserRepository;
        public CartController(IUnitOfWork unitOfWork, IApplicationUserRepository appUserRepository)
        {
            mUnitOfWork = unitOfWork;
            mAppUserRepository = appUserRepository;
        }
        public IActionResult Index()
        {
            var userId = CommonHelper.GetUserId(User);
            mCartViewModel = new CartViewModel()
            {
                ShoppingCarts = mUnitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == userId, "Product").OrderBy(u => u.ProductId),
                OrderHeader = new()
            };
            mCartViewModel.OrderHeader.OrderTotal = 0;
            foreach (var cartItem in mCartViewModel.ShoppingCarts)
            {
                mCartViewModel.OrderHeader.OrderTotal += CommonHelper.CalculateCartTotal(cartItem);
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

        public IActionResult Summary(int cartId)
        {
            var userId = CommonHelper.GetUserId(User);
            mCartViewModel = new CartViewModel()
            {
                ShoppingCarts = mUnitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == userId, "Product").OrderBy(u => u.ProductId),
                OrderHeader = new()
            };
            mCartViewModel.OrderHeader.OrderTotal = 0;
            foreach (var cartItem in mCartViewModel.ShoppingCarts)
            {
                mCartViewModel.OrderHeader.OrderTotal += CommonHelper.CalculateCartTotal(cartItem);
            }
            var applicationUser = mAppUserRepository.Get(u => u.Id == userId);

            mCartViewModel.OrderHeader.Name = applicationUser.Name;
            mCartViewModel.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
            mCartViewModel.OrderHeader.Address = applicationUser.Address;
            mCartViewModel.OrderHeader.City = applicationUser.City;
            mCartViewModel.OrderHeader.State = applicationUser.State;
            mCartViewModel.OrderHeader.PostalCode = applicationUser.PostalCode;

            return View(mCartViewModel);
        }

        #region API Calls
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
            var userId = CommonHelper.GetUserId(User);

            var shoppingCarts = mUnitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == userId, "Product").OrderBy(u => u.ProductId);
            double newTotal = 0;
            foreach (var cartItem in shoppingCarts)
            {
                newTotal += CommonHelper.CalculateCartTotal(cartItem);
            }

            return Json(new { success = true, total = newTotal });
        }
        #endregion
    }
}