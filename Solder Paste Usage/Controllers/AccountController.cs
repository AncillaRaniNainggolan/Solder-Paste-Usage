using Microsoft.AspNetCore.Mvc;
using SolderPasteUsage.Data;
using SolderPasteUsage.Models;

namespace SolderPasteUsage.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users.FirstOrDefault(x =>
                x.Username == model.Username &&
                x.PasswordHash == model.Password);

            if (user == null)
            {
                ViewBag.Error = "Username atau Password salah";
                return View(model);
            }

            HttpContext.Session.SetString(
                "Username",
                user.Username
            );

            HttpContext.Session.SetString(
                "Role",
                user.Role
            );

            if (user.Role == "Warehouse")
            {
                return RedirectToAction(
                    "Index",
                    "Warehouse"
                );
            }

            return RedirectToAction(
                "Index",
                "Dashboard"
            );
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(
                "Login",
                "Account"
            );
        }
    }

}
