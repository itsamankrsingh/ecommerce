using ECommerce.Identity.Data;
using ECommerce.Identity.Interface;
using ECommerce.Identity.Models;
using ECommerce.Utility.DataAccess.Implementation;

namespace ECommerce.Identity.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly IdentityAppDbContext mAppDb;
        public ApplicationUserRepository(IdentityAppDbContext appDb) : base(appDb)
        {
            mAppDb = appDb;
        }

        public void Update(ApplicationUser obj)
        {
            mAppDb.ApplicationUsers.Update(obj);
        }
    }
}
