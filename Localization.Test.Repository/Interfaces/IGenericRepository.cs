using System.Linq.Expressions;

namespace Localization.Test.Repository.Interfaces
{
    public interface IGenericRepository<T>
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        IEnumerable<T> GetAll();
        IQueryable<T> GetAllQueryable();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T? FindOne(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindQueryable(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        void UpdateRanage(IEnumerable<T> entities);
        void Delete(T entity);
        IQueryable<T> FindQueryableBySql(string query);
    }
}
