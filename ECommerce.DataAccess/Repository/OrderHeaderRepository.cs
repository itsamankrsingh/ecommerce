using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Interface;
using ECommerce.Models;
using ECommerce.Utility.DataAccess.Implementation;

namespace ECommerce.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext mAppDb;
        public OrderHeaderRepository(ApplicationDbContext appDb) : base(appDb)
        {
            mAppDb = appDb;
        }

        void IOrderHeaderRepository.Update(OrderHeader obj)
        {
            mAppDb.OrderHeaders.Update(obj);
        }
    }
}
