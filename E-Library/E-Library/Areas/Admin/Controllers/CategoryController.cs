using AutoMapper;
using E_Library.Models.BooksCategory;
using E_Library.Models.EntityModels;
using E_Library.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;


namespace E_Library.Areas.Admin.Controllers
{

    public class CategoryController : Controller
    {

        ICategoryService _categoryService;
        IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var user = User;
            var categories = _categoryService.Get();
            if (!categories.Any())
            {
                ViewBag.Message = "No Data Found";
                return View("_NotFound");
            }
            var categoryListVMs = new List<BookListVM>();

            foreach (var category in categories)
            {

                // var categoryVM = _mapper.Map<CategoryListVM>(category);
                var categoryVM = new BookListVM()
                {
                    Id = category.Id,
                    CategoryName = category.CategoryName
                };
                categoryListVMs.Add(categoryVM);
            }
            return View(categoryListVMs);

        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BookCreateVM createModel)
        {
            if (ModelState.IsValid)
            {
                //TODO:Model Mapping :Auto Mapper
                var category = new Category()
                {
                    CategoryName = createModel.CategoryName
                };
                var result = _categoryService.Create(category);
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
            var existingCategory = _categoryService.GetFirstOrDefault(x => x.Id == id);
            var category = new BookEditVM()
            {
                Id = existingCategory.Id,
                CategoryName = existingCategory.CategoryName

            };

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(BookEditVM editModel)
        {
            var existingCategory = _categoryService.GetFirstOrDefault(x => x.Id == editModel.Id);
            if (existingCategory == null)
            {
                ViewBag.Message = "Requested Page Not Found";
                return View("_NotFound");
            }
            existingCategory.CategoryName = editModel.CategoryName;

            var result = _categoryService.Update(existingCategory);
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
            var existingCategory = _categoryService.GetFirstOrDefault(x => x.Id == id);
            if (existingCategory == null)
            {
                ViewBag.Message = "Requested Page Not Found";
                return View("_NotFound");
            }
            var category = new BookDetailsVM()
            {
                Id = existingCategory.Id,
                CategoryName = existingCategory.CategoryName
            };

            return View(category);

        }
        public IActionResult Delete(int id)
        {
            var existingCategory = _categoryService.GetFirstOrDefault(x => x.Id == id);
            if (existingCategory == null)
            {
                ViewBag.Message = "Requested Page Not Found";
                return View("_NotFound");
            }
            var category = new BookDeleteVM()
            {
                Id = existingCategory.Id,
                CategoryName = existingCategory.CategoryName
            };
            return View(category);

        }

        [HttpPost]
        public IActionResult Delete(BookDeleteVM deleteModel)
        {

            var existingCategory = _categoryService.GetFirstOrDefault(x => x.Id == deleteModel.Id);
            if (existingCategory == null)
            {
                ViewBag.Message = "Requested Page Not Found";
                return View("_NotFound");
            }
            existingCategory.CategoryName = deleteModel.CategoryName;

            var result = _categoryService.Remove(existingCategory);
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
