using E_Library.Databases.Data;
using E_Library.Models.EntityModels;
using E_Library.Repositories.Abstractions;
using E_Library.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Repositories
{
    public class HomeRepository : Repository<IdentityUser>, IHomeRepositoy
    {

        public HomeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
