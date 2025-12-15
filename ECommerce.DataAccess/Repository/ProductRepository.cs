using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Interface;
using ECommerce.Models;

namespace ECommerce.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext mAppDb;
        public ProductRepository(ApplicationDbContext appDb) : base(appDb)
        {
            mAppDb = appDb;
        }

        void IProductRepository.Update(Product obj)
        {
            mAppDb.Products.Update(obj);
        }
    
    }
}
