using ECommerce.Models;

namespace ECommerce.Web.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        public double OrderTotal { get; set; }
    }
}
