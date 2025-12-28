using ECommerce.Models;
using System.Security.Claims;

namespace ECommerce.Utility.Helper
{
    public static class CommonHelper
    {
        public static double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            else if (shoppingCart.Count <= 100)
            {
                return shoppingCart.Product.Price50;
            }
            else
            {
                return shoppingCart.Product.Price100;
            }
        }

        public static double CalculateCartTotal(ShoppingCart shoppingCart)
        {
            shoppingCart.Price = GetPriceBasedOnQuantity(shoppingCart);
            return (shoppingCart.Price * shoppingCart.Count);
        }

        public static string? GetUserId(ClaimsPrincipal User)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}
