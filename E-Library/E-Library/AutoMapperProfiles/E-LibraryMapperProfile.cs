
using E_Library.Models.EntityModels;
using AutoMapper;
using E_Library.Models.LibraryBook;

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


            //CreateMap<CategoryListVM, Category>();
            //CreateMap<Category, CategoryListVM>();

            //CreateMap<Product, ProductListVM>();
            //CreateMap<ProductListVM, Product>();
        }
    }
}
