using Microsoft.AspNetCore.Mvc;

namespace E_Library.Areas.Student.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
