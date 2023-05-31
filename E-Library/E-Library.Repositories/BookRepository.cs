using E_Library.Databases.Data;
using E_Library.Models.EntityModels;
using E_Library.Repositories.Abstractions;
using E_Library.Repositories.Base;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {


        public BookRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
