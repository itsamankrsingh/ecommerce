using ECommerce.Models;
using ECommerce.Utility.DataAccess.Interface;

namespace ECommerce.DataAccess.Interface
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        public void Update(OrderHeader obj);
    }
}
