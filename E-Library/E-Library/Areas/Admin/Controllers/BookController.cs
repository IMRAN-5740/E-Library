using AutoMapper;
using E_Library.AppConfigurations.Utilities;
using E_Library.Models;
using E_Library.Models.BooksAuthor;
using E_Library.Models.EntityModels;
using E_Library.Models.LibraryBook;
using E_Library.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Library.Areas.Admin.Controllers
{
    public class BookController : Controller
    {
        IAuthorService _authorService;
        ICategoryService _categoryService;
        IBookService _bookService;
        IMapper _mapper;
        public BookController(IAuthorService authorService,ICategoryService categoryService, IBookService bookService,IMapper mapper)
        {
            _authorService = authorService;
            _categoryService = categoryService;
            _bookService = bookService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var books = _bookService.GetAllResult();
            if (!books.Any())
            {
                ViewBag.Message = "No Data Found";
                return View("_NotFound");
            }

            var bookListVMs = new List<BookListVM>();
           

            foreach (var book in books)
            {
             
                var bookVm = new BookListVM()
                {
                    Id= book.Id,
                    BookName = book.BookName,
                    BookCode = book.BookCode,
                    Picture = book.Picture,
                  
                    AuthorId = book.AuthorId,
                    CategoryId= book.CategoryId,
                    Authors=book.Authors,
                    Categories=book.Categories,
                    
                  
                    PublicationName = book.PublicationName,
                    Price = book.Price,
                    Quantity = book.Quantity,
                    IsAvailable = book.IsAvailable,
                };
                bookListVMs.Add(bookVm);

            }

            return View(bookListVMs);
          
        }

        [HttpGet]
        public IActionResult Create() 
        {
            
                var categories = _categoryService.Get();
                var categoriesSelectListItems = categories.Select(c => new SelectListItem()
                {
                    Text = c.Id + " " + c.CategoryName,
                    Value = c.Id.ToString()
                }).ToList();

                var bookCreateVM = new BookCreateVM();
                bookCreateVM.Categories = categoriesSelectListItems;

                var authors = _authorService.Get();
                var authorSelectListItems = authors.Select(c => new SelectListItem()
                {
                    Text = c.Id + " " +c.AuthorName,
                    Value = c.Id.ToString()
                }).ToList();

                bookCreateVM.Authors = authorSelectListItems;
                return View(bookCreateVM);

        }

        [HttpPost]
        public IActionResult Create(BookCreateVM createModel)
        {


            if (ModelState.IsValid)
            {
                var book = _mapper.Map<Book>(createModel);
                var result = _bookService.Create(book);
                if (result.IsSucced)
                {
                    //ModelState.Clear();
                    return RedirectToAction(nameof(Index));
                }
                //Utilities ErrorMessages
                ControllerUtilities.ModelStateErrorBind(result, ModelState);

            }


            var categories = _categoryService.Get();
            var categoriesSelectListItems = categories.Select(c => new SelectListItem()
            {
                Text = c.Id + " " + c.CategoryName,
                Value = c.Id.ToString()
            }).ToList();


            createModel.Categories = categoriesSelectListItems;

            var authors = _authorService.Get();
            var authorSelectListItems = authors.Select(c => new SelectListItem()
            {
                Text = c.Id + " " + c.AuthorName,
                Value = c.Id.ToString()
            }).ToList();
            createModel.Authors = authorSelectListItems;

            return View(createModel);


        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
             var existingBook=_bookService.GetFirstOrDefault(data=>data.Id==id);

            //var author = new AuthorEditVM()
            //{
            //    Id = existingAuthor.Id,
            //    AuthorName = existingAuthor.AuthorName,
            //    Description = existingAuthor.Description
            //};
            var val = existingBook.CategoryId;
            var val2 = existingBook.AuthorId;
            var categories = _categoryService.GetFirstOrDefault(c=>c.Id==val);
            //var categoriesSelectListItems = categories.Select(c => new SelectListItem()
            //{
            //    Text = c.Id + " " + c.CategoryName,
            //    Value = c.Id.ToString()
            //}).ToList();


            //existingBook.Categories = categoriesSelectListItems;

            var authors = _authorService.GetFirstOrDefault(c => c.Id == val2);
            //var authorSelectListItems = authors.Select(c => new SelectListItem()
            //{
            //    Text = c.Id + " " + c.AuthorName,
            //    Value = c.Id.ToString()
            //}).ToList();

            //bookCreateVM.Authors = authorSelectListItems;
            existingBook.Authors = authors;
            existingBook.Categories = categories;
            var book=_mapper.Map<BookEditVM>(existingBook);
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(BookEditVM editModel)
        {
            var existingBook = _bookService.GetFirstOrDefault(x => x.Id == editModel.Id);
            if (existingBook == null)
            {
                ViewBag.Message = "Requested Page Not Found";
                return View("_NotFound");
            }
           // var updateBook=_mapper.Map<BookEditVM>(existingBook);
           existingBook.Id = editModel.Id;
            existingBook.BookName = editModel.BookName;
            existingBook.BookCode=editModel.BookCode;
            existingBook.Picture=editModel.Picture;
            existingBook.Authors.AuthorName = editModel.Authors.AuthorName;
            existingBook.Categories.CategoryName = editModel.Categories.CategoryName;
            existingBook.Price = editModel.Price;
            existingBook.PublicationName = editModel.PublicationName;
            existingBook.IsAvailable = editModel.IsAvailable;
            var result = _bookService.Update(existingBook);
            if (result.IsSucced)
            {
                ModelState.Clear();
                return RedirectToAction(nameof(Index));
            }
            if (result.ErrorMessages.Any())
            {
                foreach (var error in result.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var existingBook = _bookService.GetFirstOrDefault(data => data.Id == id);

            //var author = new AuthorEditVM()
            //{
            //    Id = existingAuthor.Id,
            //    AuthorName = existingAuthor.AuthorName,
            //    Description = existingAuthor.Description
            //};
            var val = existingBook.CategoryId;
            var val2 = existingBook.AuthorId;
            var categories = _categoryService.GetFirstOrDefault(c => c.Id == val);
            //var categoriesSelectListItems = categories.Select(c => new SelectListItem()
            //{
            //    Text = c.Id + " " + c.CategoryName,
            //    Value = c.Id.ToString()
            //}).ToList();


            //existingBook.Categories = categoriesSelectListItems;

            var authors = _authorService.GetFirstOrDefault(c => c.Id == val2);
            //var authorSelectListItems = authors.Select(c => new SelectListItem()
            //{
            //    Text = c.Id + " " + c.AuthorName,
            //    Value = c.Id.ToString()
            //}).ToList();

            //bookCreateVM.Authors = authorSelectListItems;
            existingBook.Authors = authors;
            existingBook.Categories = categories;
            var book = _mapper.Map<BookDetailsVM>(existingBook);
            return View(book);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var existingBook = _bookService.GetFirstOrDefault(data => data.Id == id);

            //var author = new AuthorEditVM()
            //{
            //    Id = existingAuthor.Id,
            //    AuthorName = existingAuthor.AuthorName,
            //    Description = existingAuthor.Description
            //};
            var val = existingBook.CategoryId;
            var val2 = existingBook.AuthorId;
            var categories = _categoryService.GetFirstOrDefault(c => c.Id == val);
            //var categoriesSelectListItems = categories.Select(c => new SelectListItem()
            //{
            //    Text = c.Id + " " + c.CategoryName,
            //    Value = c.Id.ToString()
            //}).ToList();


            //existingBook.Categories = categoriesSelectListItems;

            var authors = _authorService.GetFirstOrDefault(c => c.Id == val2);
            //var authorSelectListItems = authors.Select(c => new SelectListItem()
            //{
            //    Text = c.Id + " " + c.AuthorName,
            //    Value = c.Id.ToString()
            //}).ToList();

            //bookCreateVM.Authors = authorSelectListItems;
            existingBook.Authors = authors;
            existingBook.Categories = categories;
            var book = _mapper.Map<BookDeleteVM>(existingBook);
            return View(book);
        }


    }
}
