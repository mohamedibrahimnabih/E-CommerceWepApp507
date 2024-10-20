using E_Commerce.Models;
using System.Linq.Expressions;

namespace E_Commerce.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, object>>? includeProp = null, Expression<Func<T, bool>>? expression = null);

        T? GetOne(Expression<Func<T, bool>> expression);
        void Add(T category);
        void Edit(T category);
        void Delete(T category);
        void Commit();
    }
}
