﻿using E_Library.Databases.Data;
using E_Library.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Repositories
{
    public class StudentRepository
    {
        ApplicationDbContext _dbContext; 
        public StudentRepository(ApplicationDbContext dbContext)
        {
          _dbContext = dbContext;
        }

        public bool Create(Author anAuthor)
        {
            _dbContext.Authors.Add(anAuthor);   
            return _dbContext.SaveChanges()>0;
        }

        public bool Update(Author anAuthor)
        {
            _dbContext.Authors.Update(anAuthor);
            return _dbContext.SaveChanges() > 0;
        }
        public bool Remove(Author anAuthor)
        {
            _dbContext.Authors.Remove(anAuthor);
            return _dbContext.SaveChanges() > 0;
        }
        public Author GetById(int id)
        {
            return _dbContext.Authors.FirstOrDefault(data => data.Id == id);
        }
        public ICollection<Author> GetAll()
        {
            return _dbContext.Authors.ToList();        }
    }
}
