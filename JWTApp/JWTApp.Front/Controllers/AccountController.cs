using JWTApp.Front.Models;
using Microsoft.AspNetCore.Mvc;

namespace JWTApp.Front.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View(new UserLoginModel());
        }

        [HttpPost]
        public IActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            return View(model);
        }
    }
}
