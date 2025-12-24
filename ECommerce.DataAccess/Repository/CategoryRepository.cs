using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Interface;
using ECommerce.Models;
using ECommerce.Utility.DataAccess.Implementation;

namespace ECommerce.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext mAppDb;
        public CategoryRepository(ApplicationDbContext appDb) : base(appDb)
        {
            mAppDb = appDb;
        }

        void ICategoryRepository.Update(Category obj)
        {
            mAppDb.Categories.Update(obj);
        }
    }
}
