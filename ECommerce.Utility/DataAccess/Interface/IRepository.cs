using System.Linq.Expressions;

namespace ECommerce.Utility.DataAccess.Interface
{
    public interface IRepository<T> where T : class
    {
        //T - Category
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = false);
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        public void Add(T entity);
        public void Remove(T entity);
        public void RemoveRange(IEnumerable<T> entity);
    }
}
