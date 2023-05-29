﻿using E_Library.Databases.Data;
using E_Library.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Repositories
{
    public class BookRepository
    {
        ApplicationDbContext _dbContext;
        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Create(Category aBook)
        {
             _dbContext.Categories.Add(aBook);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Update(Category aBook)
        {
            _dbContext.Categories.Update(aBook);
            return _dbContext.SaveChanges() > 0;
        }
        public bool Remove(Category aBook)
        {
            _dbContext.Categories.Remove(aBook);
            return _dbContext.SaveChanges() > 0;
        }

        public Category GeetById(int id)
        {
         return _dbContext.Categories.FirstOrDefault(data => data.Id == id);
            
        }
        public ICollection<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }
    }
}
