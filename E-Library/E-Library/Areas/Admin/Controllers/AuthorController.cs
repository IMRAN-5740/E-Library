using E_Library.Models.BookAuthor;
using E_Library.Models.EntityModels;
using E_Library.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Areas.Admin.Controllers
{
    public class AuthorController : Controller
    {

        IAuthorService _authorService;
        public AuthorController( IAuthorService authorService)
        {
            _authorService = authorService;
        }
        
        public IActionResult Index()
        {
            var authors = _authorService.Get();
            if(!authors.Any())
            {
                ViewBag.Message = "Not Data Found";
                return RedirectToAction("_NotFound");
            }
            var authorList = new List<AuthorListVM>();
            
                foreach(var author in authors)
                {
                    var addAuthor = new AuthorListVM()
                    {
                        Id = author.Id,
                        AuthorName=author.AuthorName,
                        Description =author.Description
                    };
                    authorList.Add(addAuthor);
                }
            return View(authorList);
        }
        [HttpGet]
        public ActionResult Create()
        {
              return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Author anAuthor)
        {
           

            return View(anAuthor);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Author anAuthor)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(Author anAuthor)
        {
            return View();
        }
    }
}
