using E_Library.Models;
using E_Library.Models.EntityModels;
using E_Library.Repositories.Abstractions;
using E_Library.Services.Abstractions;
using E_Library.Services.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Services
{
    public class BookService : Service<Book>, IBookService
    {
        IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository) : base(bookRepository)
        {
            _bookRepository = bookRepository;
        }


        public override Result Create(Book entity)
        {
            //unique code
            var result = new Result();
            var codeResult = _bookRepository.Get(x => x.BookCode == entity.BookCode);
            if (codeResult.Any())
            {
                result.IsSucced = false;
                result.ErrorMessages.Add("Product already Exists with same Code..!");
            }
            if (result.ErrorMessages.Any())
            {
                return result;
            }

            bool isSuccess = _bookRepository.Create(entity);
            if (isSuccess)
            {
                result.IsSucced = true;

                return result;
            }
            result.ErrorMessages.Add("Product Not Added");
            return result;
        }
        public override Result Update(Book entity)
        {
            var result = new Result();

            bool isSuccess = _bookRepository.Update(entity);
            if (isSuccess)
            {
                result.IsSucced = true;
                return result;
            }
            result.ErrorMessages.Add("Book not Updated");

            return result;
        }
        public override Result Remove(Book product)
        {
            var result = new Result();
            bool isSuccess = _bookRepository.Remove(product);
            if (isSuccess)
            {
                result.IsSucced = true;
                return result;
            }
            result.ErrorMessages.Add("Book not Removed");
            return result;
        }


    }
}
