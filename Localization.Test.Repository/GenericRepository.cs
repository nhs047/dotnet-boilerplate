using Localization.Test.Repository.DbContexts;
using Localization.Test.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Localization.Test.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LocalizationDbContext _context;
        public GenericRepository(LocalizationDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public IQueryable<T> GetAllQueryable()
        {
            return _context.Set<T>();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>()
                .AsQueryable()
                .Where(predicate).AsEnumerable();
        }

        public T? FindOne(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>()
                .AsQueryable()
                .Where(predicate).FirstOrDefault();
        }

        public IQueryable<T> FindQueryable(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>()
                .AsQueryable()
                .Where(predicate);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void UpdateRanage(IEnumerable<T> entities)
        {
            _context.UpdateRange(entities);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public IQueryable<T> FindQueryableBySql(string query)
        {
            return _context.Set<T>().FromSqlRaw(query);
        }
    }
}
