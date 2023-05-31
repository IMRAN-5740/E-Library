using E_Library.Models.BooksAuthor;
using E_Library.Models.BooksCategory;
using E_Library.Models.EntityModels;
using E_Library.Services;
using E_Library.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Areas.Admin.Controllers
{
    public class AuthorController : Controller
    {

        IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public IActionResult Index()
        {
            var authors = _authorService.Get();
            if (!authors.Any())
            {
                ViewBag.Message = "No Data Found";
                return View("_NotFound");
            }
            var authorListVMs = new List<AuthorListVM>();

            foreach (var author in authors)
            {

                // var categoryVM = _mapper.Map<CategoryListVM>(category);
                var categoryVM = new AuthorListVM()
                {
                    Id = author.Id,
                    AuthorName = author.AuthorName,
                    Description = author.Description
                };
                authorListVMs.Add(categoryVM);
            }
            return View(authorListVMs);

        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AuthorCreateVM entity)

        {
            if (ModelState.IsValid)
            {
                //TODO:Model Mapping :Auto Mapper
                var author = new Author()
                {
                    AuthorName = entity.AuthorName,
                    Description = entity.Description
                };
                var result = _authorService.Create(author);
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
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            var existingAuthor = _authorService.GetFirstOrDefault(x => x.Id == id);
            var author = new AuthorEditVM()
            {
                Id = existingAuthor.Id,
                AuthorName = existingAuthor.AuthorName,
                Description = existingAuthor.Description
            };

            return View(author);
        }


        [HttpPost]
        public IActionResult Edit(AuthorEditVM editModel)
        {
            var existingAuthor = _authorService.GetFirstOrDefault(x => x.Id == editModel.Id);
            if (existingAuthor == null)
            {
                ViewBag.Message = "Requested Page Not Found";
                return View("_NotFound");
            }
            existingAuthor.AuthorName = editModel.AuthorName;
            existingAuthor.Description = editModel.Description;

            var result = _authorService.Update(existingAuthor);
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
        public IActionResult Details(int id)
        {
            var existingAuthor = _authorService.GetFirstOrDefault(x => x.Id == id);
            if (existingAuthor == null)
            {
                ViewBag.Message = "Requested Page Not Found";
                return View("_NotFound");
            }
            var author = new AuthorDetailsVM()
            {
                Id = existingAuthor.Id,
                AuthorName = existingAuthor.AuthorName,
                Description = existingAuthor.Description
            };

            return View(author);

        }
        public IActionResult Delete(int id)
        {
            var existingAuthor = _authorService.GetFirstOrDefault(x => x.Id == id);
            if (existingAuthor == null)
            {
                ViewBag.Message = "Requested Page Not Found";
                return View("_NotFound");
            }
            var author = new AuthorDeleteVM()
            {
                Id = existingAuthor.Id,
                AuthorName = existingAuthor.AuthorName,
                Description = existingAuthor.Description
            };
            return View(author);

        }

        [HttpPost]
        public IActionResult Delete(AuthorDeleteVM deleteModel)
        {

            var existingAuthor = _authorService.GetFirstOrDefault(x => x.Id == deleteModel.Id);
            if (existingAuthor == null)
            {
                ViewBag.Message = "Requested Page Not Found";
                return View("_NotFound");
            }
            existingAuthor.AuthorName = deleteModel.AuthorName;
            existingAuthor.Description = deleteModel.Description;

            var result = _authorService.Remove(existingAuthor);
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

    }

}
