using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Interface;
using ECommerce.Models;
using ECommerce.Utility.DataAccess.Implementation;

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
            var objFromDb = mAppDb.Products.FirstOrDefault(u => u.Id == obj.Id);
            if(objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Author = obj.Author;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price100 = obj.Price100;
                objFromDb.CategoryId = obj.CategoryId;
                if (!string.IsNullOrEmpty(obj.ImageUrl))
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
            
        }
    
    }
}
