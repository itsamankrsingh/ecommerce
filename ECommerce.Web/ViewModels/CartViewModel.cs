using ECommerce.Models;

namespace ECommerce.Web.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
