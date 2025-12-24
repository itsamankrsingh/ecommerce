using ECommerce.Identity.Models;
using ECommerce.Utility.DataAccess.Interface;

namespace ECommerce.Identity.Interface
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        public void Update(ApplicationUser obj);
    }
}
