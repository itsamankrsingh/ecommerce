using System.Linq.Expressions;

namespace ECommerce.DataAccess.Interface
{
    public interface IRepository<T> where T : class
    {
        //T - Category
        public IEnumerable<T> GetAll(string? includeProperties = null);
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        public void Add(T entity);
        public void Remove(T entity);
        public void RemoveRange(IEnumerable<T> entity);
    }
}
