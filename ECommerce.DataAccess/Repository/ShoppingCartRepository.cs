using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Interface;
using ECommerce.Models;
using ECommerce.Utility.DataAccess.Implementation;

namespace ECommerce.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext mAppDb;
        public ShoppingCartRepository(ApplicationDbContext appDb) : base(appDb)
        {
            mAppDb = appDb;
        }

        void IShoppingCartRepository.Update(ShoppingCart obj)
        {
            mAppDb.ShoppingCarts.Update(obj);
        }
    }
}
