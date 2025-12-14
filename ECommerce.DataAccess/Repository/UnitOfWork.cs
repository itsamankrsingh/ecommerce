using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Interface;

namespace ECommerce.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext mAppDb;
        public ICategoryRepository Category 
        { 
            get;
            
            private set; 
        }
        public UnitOfWork(ApplicationDbContext appDb)
        {
            mAppDb = appDb;
            Category = new CategoryRepository(mAppDb);
        }

        public void Save()
        {
            mAppDb.SaveChanges();
        }
    }
}
