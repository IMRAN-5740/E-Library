using E_Library.Databases.Data;
using E_Library.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Repositories
{
    public class CategoryRepository
    {
        ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Create(Category aCategory)
        {
            _dbContext.Categories.Add(aCategory);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Update(Category aCategory)
        {
            _dbContext.Categories.Update(aCategory);
            return _dbContext.SaveChanges() > 0;
        }
        public bool Remove(Category aCategory)
        {
            _dbContext.Categories.Remove(aCategory);
            return _dbContext.SaveChanges() > 0;
        }

        public Category GeetById(int id)
        {
            return _dbContext.Categories.FirstOrDefault(data=>data.Id==id);

        }
        public ICollection<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }
    }
}
