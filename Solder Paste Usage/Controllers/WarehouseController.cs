using Microsoft.AspNetCore.Mvc;
using SolderPasteUsage.Data;

namespace SolderPasteUsage.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalOrder =
                _context.MaterialOrder.Count();

            ViewBag.Pending =
                _context.MaterialOrder.Count(x =>
                    x.TransferStatus == "Pending");

            ViewBag.Generated =
                _context.MaterialOrder.Count(x =>
                    x.TransferStatus == "Order Generated");

            ViewBag.InProgress =
                _context.MaterialOrder.Count(x =>
                    x.TransferStatus == "In Progress");

            ViewBag.Delivered =
                _context.MaterialOrder.Count(x =>
                    x.TransferStatus == "Delivered");

            ViewBag.Arrived =
                _context.MaterialOrder.Count(x =>
                    x.TransferStatus == "Arrived");

            var orders =
                _context.MaterialOrder
                .OrderByDescending(x => x.OrderDate)
                .ToList();

            return View(orders);
        }

        public IActionResult MaterialOrders()
        {
            var orders =
                _context.MaterialOrder
                .OrderByDescending(x => x.OrderDate)
                .ToList();

            return View("Index", orders);
        }

        public IActionResult Process(int id)
        {
            var order =
                _context.MaterialOrder
                .FirstOrDefault(x => x.OrderId == id);

            if (order != null)
            {
                order.TransferStatus = "In Progress";
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Deliver(int id)
        {
            var order =
                _context.MaterialOrder
                .FirstOrDefault(x => x.OrderId == id);

            if (order != null)
            {
                order.TransferStatus = "Delivered";
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Arrive(int id)
        {
            var order =
                _context.MaterialOrder
                .FirstOrDefault(x => x.OrderId == id);

            if (order != null)
            {
                order.TransferStatus = "Arrived";
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}