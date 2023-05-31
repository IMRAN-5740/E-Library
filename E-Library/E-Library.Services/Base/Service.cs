using E_Library.Models;
using E_Library.Repositories.Abstractions.Base;
using E_Library.Services.Abstractions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Services.Base
{
    public abstract class Service<T> : IService<T> where T : class
    {
        IRepository<T> _repository;
        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }
        public virtual Result Create(T entity)
        {

            var result = new Result();
            //unique code
            //var codeResult = _repository.Get();
            //if (codeResult.Any())
            //{
            //    result.IsSucced = false;
            //    result.ErrorMessages.Add("Product already Exists with same Code..!");
            //}
            //if (result.ErrorMessages.Any())
            //{
            //    return result;
            //}

            bool isSuccess = _repository.Create(entity);
            if (isSuccess)
            {
                result.IsSucced = true;
                return result;
            }
            result.ErrorMessages.Add("Could  Not Add Entity");
            return result;
        }
        public virtual Result Remove(T entity)
        {
            var result = new Result();
            bool isSuccess = _repository.Remove(entity);
            if (isSuccess)
            {
                result.IsSucced = true;
                return result;
            }
            result.ErrorMessages.Add("Could  Not Remove Entity");
            return result;
        }
        public virtual Result Update(T entity)
        {
            var result = new Result();
            bool isSuccess = _repository.Update(entity);
            if (isSuccess)
            {
                result.IsSucced = true;
                return result;
            }
            result.ErrorMessages.Add("Could  Not Update Entity");
            return result;
        }
        public virtual T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _repository.GetFirstOrDefault(predicate);
        }
        public virtual ICollection<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            return _repository.Get(predicate);
        }
    }
}
