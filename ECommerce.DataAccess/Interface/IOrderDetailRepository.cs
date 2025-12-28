using ECommerce.Models;
using ECommerce.Utility.DataAccess.Interface;

namespace ECommerce.DataAccess.Interface
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        public void Update(OrderDetail obj);
    }
}
