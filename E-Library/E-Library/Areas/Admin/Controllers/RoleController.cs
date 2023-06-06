using E_Library.Models.BooksCategory;
using E_Library.Models.EntityModels;
using E_Library.Models.Role;
using E_Library.Services;
using E_Library.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
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
               
                var roleVm = new IdentityRole()
                {
                    Name = createModel.RoleName
                };
                var result = _roleService.Create(roleVm);
                if (result.IsSucced)
                {
                    TempData["save"] = "Role Has Been Saved Successfully ";
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
            return View(createModel);
        }
        public IActionResult Edit(string id)
        {
            var existingRole = _roleService.GetFirstOrDefault(x => x.Id == id);
            var roleVM = new RoleEditVM()
            {
                Id = existingRole.Id,
                RoleName = existingRole.Name

            };

            return View(roleVM);
          
        }
        [HttpPost]
        public IActionResult Edit(RoleEditVM editModel)
        {

            if (ModelState.IsValid)
            {
                var existingRole=_roleService.GetFirstOrDefault(data=>data.Id == editModel.Id);
                if(existingRole == null)
                {
                    ViewBag.Message = "Data Not Found";
                    return View("_NotFound");
                }
                existingRole.Id= editModel.Id;
                existingRole.Name = editModel.RoleName;
                var result=_roleService.Update(existingRole);
                if (result.IsSucced)
                {
                    TempData["update"] = "Role Has Been Updated Successfully ";
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
        public IActionResult Delete(string id)
        {
            var existingRole = _roleService.GetFirstOrDefault(x => x.Id == id);
            var roleVM = new RoleDeleteVM()
            {
                Id = existingRole.Id,
                RoleName = existingRole.Name

            };

            return View(roleVM);

        }
        [HttpPost]
        public IActionResult Delete(RoleDeleteVM deleteModel)
        {
            var existingRole = _roleService.GetFirstOrDefault(x => x.Id == deleteModel.Id);
            if (existingRole == null)
            {
                ViewBag.Message = "Requested Page Not Found";
                return View("_NotFound");
            }
            //existingRole.Name = deleteModel.RoleName;

            var result = _roleService.Remove(existingRole);
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
