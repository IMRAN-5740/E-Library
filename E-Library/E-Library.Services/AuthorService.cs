using E_Library.Models;
using E_Library.Models.EntityModels;
using E_Library.Repositories.Abstractions;
using E_Library.Services.Abstractions;
using E_Library.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Services
{
    public class AuthorService : Service<Author>, IAuthorService
    {
        IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository) : base(authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public override Result Create(Author entity)
        {
            //unique code
            var result = new Result();
            var codeResult = _authorRepository.Get(x => x.AuthorName == entity.AuthorName);
            if (codeResult.Any())
            {
                result.IsSucced = false;
                result.ErrorMessages.Add("Author  already Exists with same Name ..!");
            }
            if (result.ErrorMessages.Any())
            {
                return result;
            }

            bool isSuccess = _authorRepository.Create(entity);
            if (isSuccess)
            {
                result.IsSucced = true;
            }
            result.ErrorMessages.Add("Author Not Added");
            return result;
        }
        public override Result Update(Author entity)
        {
            var result = new Result();

            bool isSuccess = _authorRepository.Update(entity);
            if (isSuccess)
            {
                result.IsSucced = true;
            }
            result.ErrorMessages.Add("Author not Updated");

            return result;
        }
        public override Result Remove(Author entity)
        {
            var result = new Result();
            bool isSuccess = _authorRepository.Remove(entity);
            if (isSuccess)
            {
                result.IsSucced = true;
            }
            result.ErrorMessages.Add("Author not Removed");
            return result;
        }
    }
}
