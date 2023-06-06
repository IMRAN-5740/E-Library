using E_Library.Databases.Data;
using E_Library.Models.AuthModels;
using E_Library.Repositories;
using E_Library.Repositories.Abstractions;
using E_Library.Services;
using E_Library.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace E_Library.AppConfigurations
{
    public class E_LibraryAppConfiguration
    {
        public static void ConfigureDependencies(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                string connectionString = "Server=MOHAMMAD-IMRAN;Database=E-Library;User Id=sa;Password=imran;TrustServerCertificate=True;MultipleActiveResultSets=true";
                options.UseSqlServer(connectionString);
            });
            //dependency resolving mechanisms



           // services.AddTransient<IPasswordHasher<ApplicationUser>>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IAuthorService, AuthorService>();

            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IBookService, BookService>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryService, CategoryService>();

            services.AddTransient<IIssuesRepository, IssuesRepository>();
            services.AddTransient<IIssuesService, IssuesService>();

            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IRoleService, RoleService>();



        }
    }
}
