using E_Library.Models.Role;
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
    }
}
