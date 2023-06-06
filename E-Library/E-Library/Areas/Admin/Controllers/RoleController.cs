using E_Library.Models.EntityModels;
using E_Library.Models.Role;
using E_Library.Services;
using E_Library.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
                _roleService = roleService;
        }
        public IActionResult Index()
        {
            var roleLists = _roleService.Get();
            if(!roleLists.Any())
            {
                ViewBag.Message = "Data Not Found";
                return View("_NotFound");
            }
            var roleListVMs = new List<RoleListVM>();
            foreach (var role in roleLists)
            {
                var roleVM = new RoleListVM()
                {
                    Id = role.Id,
                    RoleName = role.Name,
                };
                roleListVMs.Add(roleVM);
            }
            return View(roleListVMs);
        }
        public IActionResult Create()
        {
            return View();        
        }

        [HttpPost]
        public IActionResult Create(RoleCreateVM createModel)
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
        }
    }
}
