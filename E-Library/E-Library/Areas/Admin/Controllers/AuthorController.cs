using E_Library.Models.EntityModels;
using E_Library.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Areas.Admin.Controllers
{
    public class AuthorController : Controller
    {
        AuthorRepository _authorRepository;
   
        public AuthorController(AuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
          
                
        }
        public IActionResult Index()
        {
           var authorList = _authorRepository.GetAll();
            return View(authorList);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Author anAuthor)
        {
            var deployAuthor = new Author()
            {
                AuthorName = anAuthor.AuthorName 
            };
            var isSucced=_authorRepository.Create(deployAuthor);
            if(isSucced)
            {
                ViewBag["create"] = "Author Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
            
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
