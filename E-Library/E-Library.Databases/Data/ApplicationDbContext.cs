using E_Library.Models.AuthModels;
using E_Library.Models.EntityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Databases.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book>Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssuesBook> IssuesBooks { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

       
        
    }
}