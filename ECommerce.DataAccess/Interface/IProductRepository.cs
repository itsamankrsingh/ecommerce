using ECommerce.Models;

namespace ECommerce.DataAccess.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        public void Update(Product obj);
    }
}
