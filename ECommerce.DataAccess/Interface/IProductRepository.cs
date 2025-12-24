using ECommerce.Models;
using ECommerce.Utility.DataAccess.Interface;

namespace ECommerce.DataAccess.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        public void Update(Product obj);
    }
}
