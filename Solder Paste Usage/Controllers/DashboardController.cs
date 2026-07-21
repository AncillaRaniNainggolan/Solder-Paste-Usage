using Microsoft.AspNetCore.Mvc;
using SolderPasteUsage.Data;

namespace SolderPasteUsage.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var dashboardData =

                (from mo in _context.MaterialOrder

                 join pm in _context.ProductMaster
                 on mo.ProjectName equals pm.ProjectName

                 select new
                 {
                     mo.WO,
                     mo.ProjectName,
                     pm.SPPN,
                     pm.CPN,

                     pm.VolumePerPcs,
                     pm.SolderPastePerPcs,

                     TotalVolume =
                        pm.VolumePerPcs * mo.QtyJar,

                     mo.QtyJar,
                     mo.TransferStatus,
                     mo.OrderDate
                 })

                 .ToList();

            // ==========================
            // KPI
            // ==========================

            ViewBag.TotalSKID =
                dashboardData.Sum(x => x.QtyJar);

            ViewBag.SolderUsage =
                dashboardData.Sum(x => x.SolderPastePerPcs);

            ViewBag.TotalVolume =
                dashboardData.Sum(x => x.TotalVolume);

            ViewBag.UsageRate =
                dashboardData.Sum(x => x.TotalVolume) == 0
                ? 0
                : dashboardData.Sum(x => x.SolderPastePerPcs)
                  / dashboardData.Sum(x => x.TotalVolume);

            // ==========================
            // CHART LABEL
            // ==========================

            ViewBag.MonthLabels = new[]
            {
            "Jan",
            "Feb",
            "Mar",
            "Apr",
            "May",
            "Jun",
            "Jul",
            "Aug",
            "Sep",
            "Oct",
            "Nov",
            "Dec"
        };

            // ==========================
            // MONTHLY USAGE FROM DB
            // ==========================

            var monthlyUsage = new int[12];

            var chartData =

                _context.MaterialOrder

                .GroupBy(x => x.OrderDate.Month)

                .Select(x => new
                {
                    Month = x.Key,
                    TotalJar = x.Sum(y => y.QtyJar)
                })

                .ToList();

            foreach (var item in chartData)
            {
                monthlyUsage[item.Month - 1]
                    = item.TotalJar;
            }

            ViewBag.MonthUsage =
                monthlyUsage;

            // ==========================
            // PIE CHART
            // ==========================

            ViewBag.LineLabels = new[]
            {
            "Line01",
            "Line02",
            "Line03",
            "Line04",
            "Line05"
        };

            ViewBag.LineData = new[]
            {
            25,
            18,
            22,
            15,
            20
        };

            return View(
                dashboardData
            );
        }
    }

}
