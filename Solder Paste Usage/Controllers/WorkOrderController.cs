using Microsoft.AspNetCore.Mvc;
using SolderPasteUsage.Data;

namespace SolderPasteUsage.Controllers
{
    public class WorkOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data =
                _context.WorkOrder
                .OrderBy(x => x.WoNumber)
                .ToList();

            return View(data);
        }
    }
}