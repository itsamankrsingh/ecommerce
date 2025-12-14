using ECommerce.Models;

namespace ECommerce.DataAccess.Interface
{
    public interface ICategoryRepository: IRepository<Category>
    {
        public void Update(Category obj);
    }
}
