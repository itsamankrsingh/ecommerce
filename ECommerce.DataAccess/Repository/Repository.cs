using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext mAppDb;
        internal DbSet<T> mDbSet;
        public Repository(ApplicationDbContext appDb)
        {
            mAppDb = appDb;
            this.mDbSet = mAppDb.Set<T>(); //same as mAppDb.Categories for Category entity
        }

        public void Add(T entity)
        {
            mAppDb.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return mDbSet.AsNoTracking().FirstOrDefault(filter);
        }

        public IEnumerable<T> GetAll()
        {
            return mDbSet.AsNoTracking().ToList();
        }

        public void Remove(T entity)
        {
            mDbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            mDbSet.RemoveRange(entity);
        }
    }
}