using Microsoft.AspNetCore.Mvc;
using SolderPasteUsage.Data;

namespace SolderPasteUsage.Controllers
{
    public class MaterialOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaterialOrderController(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            var orders =
                _context.MaterialOrder
                .OrderByDescending(x => x.AutoOrderTime)
                .ToList();

            return View(orders);
        }
    }
}