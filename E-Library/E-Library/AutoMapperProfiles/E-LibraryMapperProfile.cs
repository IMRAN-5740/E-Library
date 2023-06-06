
using E_Library.Models.EntityModels;
using AutoMapper;
using E_Library.Models.LibraryBook;
using E_Library.Models.BooksAuthor;
using E_Library.Models.AuthUser;
using Microsoft.AspNetCore.Identity;
using E_Library.Models.AuthModels;

namespace E_Library.AutoMapperProfiles
{
    public class E_LibraryMapperProfile:Profile
    {
        public E_LibraryMapperProfile()
        {

            CreateMap<BookCreateVM, Book>();
            CreateMap<Book, BookCreateVM>();

            CreateMap<BookListVM, Book>();
            CreateMap<Book, BookListVM>();

            
            CreateMap<BookEditVM, Book>();
            CreateMap<Book,BookEditVM>();
            CreateMap<BookDetailsVM, Book>();
            CreateMap<Book, BookDetailsVM>();

            CreateMap<BookDeleteVM, Book>();
            CreateMap<Book, BookDeleteVM>();


            CreateMap<Author, AuthorCreateVM>();
            CreateMap<AuthorCreateVM, Author>();


            CreateMap<Author, AuthorDeleteVM>();
            CreateMap<AuthorDeleteVM, Author>();

            CreateMap<Author, AuthorDetailsVM>();
            CreateMap<AuthorDetailsVM, Author>();
            CreateMap<Author, AuthorEditVM>();
            CreateMap<AuthorEditVM, Author>();
            CreateMap<UserIndexVM, ApplicationUser>();
            CreateMap<ApplicationUser, UserIndexVM>();

            CreateMap<UserCreateVM, ApplicationUser>();
            CreateMap<ApplicationUser, UserCreateVM>();

            //CreateMap<CategoryListVM, Category>();
            //CreateMap<Category, CategoryListVM>();

            //CreateMap<Product, ProductListVM>();
            //CreateMap<ProductListVM, Product>();
        }
    }
}
