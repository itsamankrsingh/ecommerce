using ECommerce.Models;

namespace ECommerce.DataAccess.Interface
{
    public interface ICompanyRepository : IRepository<Company>
    {
        public void Update(Company obj);
    }
}
