using E_Library.Models.EntityModels;
using E_Library.Repositories.Base;
using E_Library.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using E_Library.Databases.Data;
using E_Library.Repositories.Abstractions;

namespace E_Library.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {


        public AuthorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
