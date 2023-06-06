using AutoMapper;
using E_Library.AppConfigurations.Utilities;
using E_Library.Models;
using E_Library.Models.BooksAuthor;
using E_Library.Models.EntityModels;
using E_Library.Models.LibraryBook;
using E_Library.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace E_Library.Areas.Admin.Controllers
{
    public class BookController : Controller
    {
        IAuthorService _authorService;
        ICategoryService _categoryService;
        IBookService _bookService;
        IMapper _mapper;
        private IHostingEnvironment _he;
        public BookController(IAuthorService authorService,ICategoryService categoryService, IBookService bookService,IMapper mapper,IHostingEnvironment he)
        {
            _authorService = authorService;
            _categoryService = categoryService;
            _bookService = bookService;
            _mapper = mapper;
            _he= he;
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
        public IActionResult Create(BookCreateVM createModel,IFormFile image)
        {





            //var categories = _categoryService.Get();
            //var categoriesSelectListItems = categories.Select(c => new SelectListItem()
            //{
            //    Text = c.Id + " " + c.CategoryName,
            //    Value = c.Id.ToString()
            //}).ToList();


            //createModel.Categories = categoriesSelectListItems;

            //var authors = _authorService.Get();
            //var authorSelectListItems = authors.Select(c => new SelectListItem()
            //{
            //    Text = c.Id + " " + c.AuthorName,
            //    Value = c.Id.ToString()
            //}).ToList();
            //createModel.Authors = authorSelectListItems;

            if (image != null)
            {
                var name = Path.Combine(_he.WebRootPath + "/BookImages", Path.GetFileName(image.FileName));
                var stream = new FileStream(name, FileMode.Create);
                 image.CopyToAsync(stream);
                createModel.Picture = "BookImages/" + image.FileName;
                stream.Close();
            }
            if (image == null)
            {
                createModel.Picture = "BookImages/No-Image.png";
            }

            var book = _mapper.Map<Book>(createModel);
                var result = _bookService.Create(book);
                if (result.IsSucced)
                {
                    //ModelState.Clear();
                    return RedirectToAction(nameof(Index));
                }
                //Utilities ErrorMessages
                ControllerUtilities.ModelStateErrorBind(result, ModelState);

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
        public IActionResult Edit(BookEditVM editModel,IFormFile picture)
        {

            if (picture != null)
            {
                var name = Path.Combine(_he.WebRootPath + "/BookImages", Path.GetFileName(picture.FileName));

                string oldImage = Path.Combine(_he.WebRootPath + "/" + editModel.Picture);

                if (System.IO.File.Exists(oldImage) && editModel.Picture != "BookImages/No-Image.png")
                {
                    System.IO.File.Delete(oldImage);
                }
                var stream = new FileStream(name, FileMode.Create);
                picture.CopyToAsync(stream);
                editModel.Picture = "BookImages/" + picture.FileName;
                stream.Close();
            }

            if (picture == null)
            {
                string oldImage = editModel.Picture;
                editModel.Picture = oldImage;

            }



            var existingBook = _bookService.GetFirstOrDefault(x => x.Id == editModel.Id);
            if (existingBook == null)
            {
                ViewBag.Message = "Requested Page Not Found";
                return View("_NotFound");
            }



            // var updateBook=_mapper.Map<BookEditVM>(existingBook);
            BookBinding(editModel, existingBook);
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

        private static void BookBinding(BookEditVM editModel, Book existingBook)
        {
            existingBook.Id = editModel.Id;
            existingBook.BookName = editModel.BookName;
            existingBook.BookCode = editModel.BookCode;
            existingBook.Picture = editModel.Picture;
            existingBook.Authors.AuthorName = editModel.Authors.AuthorName;
            existingBook.Categories.CategoryName = editModel.Categories.CategoryName;
            existingBook.Price = editModel.Price;
            existingBook.PublicationName = editModel.PublicationName;
            existingBook.Quantity = editModel.Quantity;
            existingBook.IsAvailable = editModel.IsAvailable;
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
          

            var authors = _authorService.GetFirstOrDefault(c => c.Id == val2);
           
            existingBook.Authors = authors;
            existingBook.Categories = categories;
            var book = _mapper.Map<BookDetailsVM>(existingBook);
            return View(book);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var existingBook = _bookService.GetFirstOrDefault(data => data.Id == id);

           
            var val = existingBook.CategoryId;
            var val2 = existingBook.AuthorId;
            var categories = _categoryService.GetFirstOrDefault(c => c.Id == val);
          

           
            var authors = _authorService.GetFirstOrDefault(c => c.Id == val2);
           
            existingBook.Authors = authors;
            existingBook.Categories = categories;
            var book = _mapper.Map<BookDeleteVM>(existingBook);
            return View(book);
        }
        [HttpPost]
        public IActionResult Delete(BookDeleteVM deleteModel,int ?id, IFormFile image)
        {
            //if(ModelState.IsValid)
            //{

            if (id == null)
            {
                ViewBag.Message = "No Data Found";
                return View("_NotFound");
            }
            if (id != deleteModel.Id)
            {
                ViewBag.Message = "No Data Found";
                return View("_NotFound");
            }
            var existingBook=_bookService.GetFirstOrDefault(data=>data.Id==deleteModel.Id);
                if(existingBook==null)
                {
                    ViewBag.Message = "No Data Found";
                    return View("_NotFound");
                }
            if (image == null)
            {

                string oldImage = Path.Combine(_he.WebRootPath + "/" + existingBook.Picture);

                if (System.IO.File.Exists(oldImage))
                {
                    System.IO.File.Delete(oldImage);
                }

            }
            BookBinding1(deleteModel, existingBook);
                var result=_bookService.Remove(existingBook);
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

            //}
            return View(deleteModel);
        }
        private static void BookBinding1(BookDeleteVM editModel, Book existingBook)
        {
            existingBook.Id = editModel.Id;
            existingBook.BookName = editModel.BookName;
            existingBook.BookCode = editModel.BookCode;
            existingBook.Picture = editModel.Picture;
            existingBook.Authors.AuthorName = editModel.Authors.AuthorName;
            existingBook.Categories.CategoryName = editModel.Categories.CategoryName;
            existingBook.Price = editModel.Price;
            existingBook.PublicationName = editModel.PublicationName;
            existingBook.IsAvailable = editModel.IsAvailable;
        }

    }
}
