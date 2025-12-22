using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Interface;
using ECommerce.Models;

namespace ECommerce.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext mAppDb;
        public CompanyRepository(ApplicationDbContext appDb) : base(appDb)
        {
            mAppDb = appDb;
        }

        void ICompanyRepository.Update(Company obj)
        {
            mAppDb.Companies.Update(obj);
        }
    }
}
