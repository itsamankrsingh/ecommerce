using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Interface;
using ECommerce.Models;
using ECommerce.Utility.DataAccess.Implementation;

namespace ECommerce.DataAccess.Repository
{
    internal class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext mAppDb;
        public OrderDetailRepository(ApplicationDbContext appDb) : base(appDb)
        {
            mAppDb = appDb;
        }

        void IOrderDetailRepository.Update(OrderDetail obj)
        {
            mAppDb.OrderDetails.Update(obj);
        }
    }
}
