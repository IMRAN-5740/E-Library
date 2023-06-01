using Microsoft.EntityFrameworkCore;
using E_Library.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using E_Library.Databases.Data;
using E_Library.Models.EntityModels;

namespace E_Library.Repositories.Base
{
    public class Repository<T> where T : class
    {
        protected ApplicationDbContext _dbContext;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private DbSet<T> Table
        {
            get
            {
                return _dbContext.Set<T>();
            }
        }
        public bool Create(T entity)
        {
            Table.Add(entity);
            return _dbContext.SaveChanges() > 0;

        }
        public bool Update(T entity)
        {
            Table.Update(entity);
            return _dbContext.SaveChanges() > 0;
        }
        public bool Remove(T entity)
        {
            Table.Remove(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return Table.FirstOrDefault(predicate);

        }
        public ICollection<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return Table.ToList();
            }
            return Table.Where(predicate).ToList();
        }
        public ICollection<T> GetAllResult(Expression<Func<T, bool>> predicate)
        {
            if(predicate== null)
            {
                return (ICollection<T>)_dbContext.Books.Include(c=>c.Authors).Include(data=>data.Categories).ToList();
            }
            return Table.Where(predicate).ToList();
        }

    }
}
