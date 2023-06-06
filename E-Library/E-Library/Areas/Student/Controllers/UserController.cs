using AutoMapper;
using E_Library.Models;
using E_Library.Models.AuthModels;
using E_Library.Models.AuthUser;
using E_Library.Models.BooksCategory;
using E_Library.Services;
using E_Library.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace E_Library.Areas.Student.Controllers
{
    public class UserController : Controller
    {
        UserManager<IdentityUser> _userManager;
        IUserService _userService;
        IHttpContextAccessor _httpContextAccessor;
        IMapper _mapper;
       // IPasswordHasher<ApplicationUser> _passwordHasher;
        public UserController(UserManager<IdentityUser> userManager, IUserService userService, IHttpContextAccessor httpContextAccessor,IMapper mapper/*,IPasswordHasher<ApplicationUser> passwordHasher*/)
        {

            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;
            //_passwordHasher = passwordHasher;

        }
        public IActionResult Index()
        {
            var user = User;
            var users = _userService.Get();
            if (!users.Any())
            {
                ViewBag.Message = "No Data Found";
                return View("_NotFound");
            }
            var userListVMs = new List<UserIndexVM>();

            foreach (var userInfo in users)
            {

                //var userVM = _mapper.Map<UserIndexVM>(userInfo);
                var userVM = new UserIndexVM()
                {
                   Id = userInfo.Id,
                   UserName=userInfo.UserName,
                   FirstName=userInfo.FirstName,
                   LastName=userInfo.LastName,
                   MiddleName=userInfo.MiddleName,
                   RegNo=userInfo.RegNo,
                   DeptName=userInfo.DeptName,
                   PhoneNumber=userInfo.PhoneNumber,
                   LockoutEnd = userInfo.LockoutEnd,
                   Email=userInfo.Email,
                   Address=userInfo.Address
                };
                userListVMs.Add(userVM);
            }
            return View(userListVMs);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserCreateVM createModel)
        {
            if (ModelState.IsValid)
            {
                
                var mainUser=_mapper.Map<ApplicationUser>(createModel);
                //mainUser.PasswordHash = _passwordHasher.HashPassword(mainUser, createModel.Password);
                mainUser.PasswordHash = createModel.Password;
                var result= _userService.Create(mainUser);
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
        [HttpGet]
        public IActionResult Edit(string id)
        {

            var existingUser = _userService.GetFirstOrDefault(c=>c.Id==id);
            if(existingUser==null)
            {
                ViewBag.Message = "Data not  Found";
                return View("_NotFound");

            }
            var userEditVMs = new UserEditVM()
            {
                UserName = existingUser.UserName,
                FirstName = existingUser.FirstName,
                MiddleName = existingUser.MiddleName,
                LastName = existingUser.LastName,
                Email = existingUser.Email,
                PhoneNumber = existingUser.PhoneNumber,
                Address = existingUser.Address,
                RegNo = existingUser.RegNo,
                DeptName = existingUser.DeptName
            };
            return View(userEditVMs);
        }
        [HttpPost]
        public IActionResult Edit(UserEditVM editModel)
        {
            if(ModelState.IsValid)
            {
                var existingUser = _userService.GetFirstOrDefault(data => data.Id == editModel.Id);
                if (existingUser == null)
                {
                    ViewBag.Message = "Data not  Found";
                    return View("_NotFound");
                }

                existingUser.UserName = editModel.UserName;
                existingUser.FirstName = editModel.FirstName;
                existingUser.MiddleName = editModel.MiddleName;
                existingUser.LastName = editModel.LastName;
                existingUser.Email = editModel.Email;
                existingUser.DeptName = editModel.DeptName;
                existingUser.RegNo = editModel.RegNo;
                existingUser.Address = editModel.Address;
                existingUser.PhoneNumber = editModel.PhoneNumber;
                var existPassword = existingUser.PasswordHash;
                if (editModel.OldPassword != null && editModel.OldPassword == existPassword)
                {
                    existingUser.PasswordHash = editModel.NewPassword;
                }
                var result = _userService.Update(existingUser);
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
        [HttpGet]
        public IActionResult Details(string id)
        {
            var existingUser = _userService.GetFirstOrDefault(c => c.Id == id);
            if (existingUser == null)
            {
                ViewBag.Message = "Data not  Found";
                return View("_NotFound");

            }
            var userEditVMs = new UserDetailsVM()
            {
                Id=existingUser.Id,
                UserName = existingUser.UserName,
                FirstName = existingUser.FirstName,
                MiddleName = existingUser.MiddleName,
                LastName = existingUser.LastName,
                Email = existingUser.Email,
                PhoneNumber = existingUser.PhoneNumber,
                Address = existingUser.Address,
                RegNo = existingUser.RegNo,
                DeptName = existingUser.DeptName
            };
            return View(userEditVMs);
            

        }
        [HttpGet]
        public IActionResult LockOut(string id)
        {


            if (id == null)
            {
                ViewBag.Message = "Data Not Found";
                return View("_NotFound");
            }
            var existingUser = _userService.GetFirstOrDefault(data=>data.Id == id);

            if (existingUser == null)
            {

                ViewBag.Message = "Data Not Found";
                return View("_NotFound");
            }
            //string myUserName = _httpContextAccessor.HttpContext.User.Identity.Name;
            //if (myUserName == "admin@gmail.com")
            //{
            //    return View(info);
            //}

            //if (info.UserName != "admin@gmail.com")
            //{
            //    return RedirectToAction("AccessDenied", "Account", new { Area = "Identity" });
            //}
            var userActiveFormVMs = new UserLockOutVM()
            {
                Id = existingUser.Id,
                UserName = existingUser.UserName,
                FirstName = existingUser.FirstName,
                MiddleName = existingUser.MiddleName,
                LastName = existingUser.LastName,
                Email = existingUser.Email,
                PhoneNumber = existingUser.PhoneNumber,
                Address = existingUser.Address,
                RegNo = existingUser.RegNo,
                DeptName = existingUser.DeptName
            };
            return View(userActiveFormVMs);
           
        }
        [HttpPost]
        
        public async Task<IActionResult> LockOut(UserLockOutVM applicationUser)
        {
            var userInfo = _userService.GetFirstOrDefault(data => data.Id == applicationUser.Id);
            if (userInfo == null)
            {
                ViewBag.Message = "Data Not Found";
                return View("_NotFound");
            }
            userInfo.LockoutEnd = DateTime.Now.AddDays(15);
            var result = _userService.Update(userInfo);
            if (result.IsSucced)
            {
                TempData["lock"] = "User Has Been LockOut Successfully";
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
            return View(userInfo);
        }

        public async Task<IActionResult> Active(string id)
        {
            var existingUser = _userService.GetFirstOrDefault(c => c.Id == id);
            if (existingUser == null)
            {
                ViewBag.Message = "Data not  Found";
                return View("_NotFound");

            }
            var userActiveFormVMs = new UserActiveFormVM()
            {
                Id = existingUser.Id,
                UserName = existingUser.UserName,
                FirstName = existingUser.FirstName,
                MiddleName = existingUser.MiddleName,
                LastName = existingUser.LastName,
                Email = existingUser.Email,
                PhoneNumber = existingUser.PhoneNumber,
                Address = existingUser.Address,
                RegNo = existingUser.RegNo,
                DeptName = existingUser.DeptName
            };
            return View(userActiveFormVMs);

        }

        [HttpPost]
       
        public async Task<IActionResult> Active(UserActiveFormVM applicationUser)
        {
            var userInfo = _userService.GetFirstOrDefault(data => data.Id == applicationUser.Id);
            if (userInfo == null)
            {
                return NotFound();
            }
            userInfo.LockoutEnd = null;
            var result = _userService.Update(userInfo);
            if (result.IsSucced)
            {
                TempData["lock"] = "User Has Been Activated Successfully";
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
            return View(userInfo);
        }


        [HttpGet]
        public IActionResult Delete(string id)
        {
            var existingUser = _userService.GetFirstOrDefault(c => c.Id == id);
            if (existingUser == null)
            {
                ViewBag.Message = "Data not  Found";
                return View("_NotFound");

            }
            var userDeleteVMs = new UserDeleteVM()
            {
                Id = existingUser.Id,
                UserName = existingUser.UserName,
                FirstName = existingUser.FirstName,
                MiddleName = existingUser.MiddleName,
                LastName = existingUser.LastName,
                Email = existingUser.Email,
                PhoneNumber = existingUser.PhoneNumber,
                Address = existingUser.Address,
                RegNo = existingUser.RegNo,
                DeptName = existingUser.DeptName
            };
            return View(userDeleteVMs);
        }

        [HttpPost]
        public IActionResult Delete(UserDeleteVM deleteModel) 
        {

            if (ModelState.IsValid)
            {
                var userInfo = _userService.GetFirstOrDefault(data => data.Id == deleteModel.Id);
                if (userInfo == null)
                {
                    ViewBag.Message = "Data not  Found";
                    return View("_NotFound");
                }
                if (deleteModel.Id != userInfo.Id)
                {
                    ViewBag.Message = "Data not  Found";
                    return View("_NotFound");
                }
                if (deleteModel == null)
                {
                    ViewBag.Message = "Data not  Found";
                    return View("_NotFound");
                }

                var result = _userService.Remove(userInfo);
                if (result.IsSucced)
                {
                    TempData["Delete"] = "User has been Deleted Successfully";
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
                return View(userInfo);
               
            }

            return View();
        }
            
        
       
       
    }

}
