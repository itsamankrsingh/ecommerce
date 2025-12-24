using ECommerce.Models;
using ECommerce.Utility.DataAccess.Interface;

namespace ECommerce.DataAccess.Interface
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        public void Update(ShoppingCart obj);
    }
}
