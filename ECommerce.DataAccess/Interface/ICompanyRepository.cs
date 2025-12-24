using ECommerce.Models;
using ECommerce.Utility.DataAccess.Interface;

namespace ECommerce.DataAccess.Interface
{
    public interface ICompanyRepository : IRepository<Company>
    {
        public void Update(Company obj);
    }
}
