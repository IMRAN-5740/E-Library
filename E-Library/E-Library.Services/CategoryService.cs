
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using E_Library.Repositories.Abstractions;
using E_Library.Models.EntityModels;
using E_Library.Services.Base;
using E_Library.Services.Abstractions;
using E_Library.Models;

namespace E_Library.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public override Result Create(Category entity)
        {
            var result = new Result();
            //code unique
            var codeResult = _categoryRepository.Get(c => c.CategoryName == entity.CategoryName);
            if (codeResult.Any())
            {
                result.IsSucced = false;
                result.ErrorMessages.Add("Category already Exists with same Name..!");
            }


            if (result.ErrorMessages.Any())
            {
                return result;
            }
            bool isSuccess = _categoryRepository.Create(entity);
            if (isSuccess)
            {
                result.IsSucced = true;
                return result;
            }
            result.ErrorMessages.Add("Category not Created");

            return result;
        }
        public override Result Update(Category entity)
        {
            var result = new Result();
            bool isSuccess = _categoryRepository.Update(entity);

            if (isSuccess)
            {
                result.IsSucced = true;
                return result;
            }

            result.ErrorMessages.Add("Category not Updated");

            return result;
        }
        public override Result Remove(Category entity)
        {
            var result = new Result();
            bool isSuccess = _categoryRepository.Remove(entity);
            if (isSuccess)
            {
                result.IsSucced = true;
                return result;
            }

            result.ErrorMessages.Add("Category not Removed");

            return result;
        }
    }
}
