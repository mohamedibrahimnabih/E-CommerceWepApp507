using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Commerce.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        ApplicationDbContext dbContext;// = new ApplicationDbContext();
        DbSet<T> dbSet;
        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        // CRUD
        public IEnumerable<T> GetAll(Expression<Func<T, object>>? includeProp = null, Expression<Func<T, bool>>? expression = null)
        {
            IQueryable query = dbSet;
            if (includeProp != null)
            {
                dbSet.Include(includeProp);
            }

            return expression == null ? dbSet.ToList() : dbSet.Where(expression).ToList();
        }

        public T? GetOne(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression).FirstOrDefault();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Edit(T entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }
    }
}
