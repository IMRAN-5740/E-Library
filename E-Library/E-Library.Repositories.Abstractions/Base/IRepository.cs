using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Repositories.Abstractions.Base
{
    public interface IRepository<T> where T : class
    {
        bool Create(T entity);
        bool Update(T entity);
        bool Remove(T entity);


        T GetFirstOrDefault(Expression<Func<T, bool>> predicate);
        ICollection<T> Get(Expression<Func<T, bool>> predicate = null);
        ICollection<T> GetAllResult(Expression<Func<T, bool>> predicate = null);
    }
}
