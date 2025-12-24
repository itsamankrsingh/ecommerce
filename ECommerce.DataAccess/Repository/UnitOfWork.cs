using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Interface;

namespace ECommerce.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext mAppDb;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public UnitOfWork(ApplicationDbContext appDb)
        {
            mAppDb = appDb;
            Category = new CategoryRepository(mAppDb);
            Product = new ProductRepository(mAppDb);
            Company = new CompanyRepository(mAppDb);
            ShoppingCart = new ShoppingCartRepository(mAppDb);
        }

        public void Save()
        {
            mAppDb.SaveChanges();
        }
    }
}