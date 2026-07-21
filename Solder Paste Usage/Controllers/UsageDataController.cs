using Microsoft.AspNetCore.Mvc;
using SolderPasteUsage.Data;

namespace SolderPasteUsage.Controllers
{
    public class UsageDataController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsageDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Daily(string period)
        {
            DateTime targetDate =
                period == "yesterday"
                ? DateTime.Today.AddDays(-1)
                : DateTime.Today;

            return LoadUsageData(
                x => x.WOStart.Date == targetDate,
                $"Daily - {(period == "yesterday" ? "Yesterday" : "Today")}"
            );
        }

        public IActionResult Weekly(string period)
        {
            DateTime today = DateTime.Today;

            DateTime startWeek =
                today.AddDays(-(int)today.DayOfWeek);

            DateTime endWeek =
                startWeek.AddDays(7);

            if (period == "lastweek")
            {
                startWeek = startWeek.AddDays(-7);
                endWeek = startWeek.AddDays(7);
            }

            return LoadUsageData(
                x => x.WOStart >= startWeek &&
                     x.WOStart < endWeek,
                $"Weekly - {(period == "lastweek" ? "Last Week" : "This Week")}"
            );
        }

        public IActionResult Monthly(string period)
        {
            int month =
                period == "lastmonth"
                ? DateTime.Now.AddMonths(-1).Month
                : DateTime.Now.Month;

            int year =
                period == "lastmonth"
                ? DateTime.Now.AddMonths(-1).Year
                : DateTime.Now.Year;

            return LoadUsageData(
                x => x.WOStart.Month == month &&
                     x.WOStart.Year == year,
                $"Monthly - {(period == "lastmonth" ? "Last Month" : "This Month")}"
            );
        }

        public IActionResult Quarterly(string period)
        {
            int quarter =
                ((DateTime.Now.Month - 1) / 3) + 1;

            if (period == "lastquarter")
                quarter--;

            return LoadUsageData(
                x => (((x.WOStart.Month - 1) / 3) + 1) == quarter,
                $"Quarterly - {(period == "lastquarter" ? "Last Quarter" : "This Quarter")}"
            );
        }

        public IActionResult Yearly(string period)
        {
            int year =
                period == "lastyear"
                ? DateTime.Now.Year - 1
                : DateTime.Now.Year;

            return LoadUsageData(
                x => x.WOStart.Year == year,
                $"Yearly - {(period == "lastyear" ? "Last Year" : "This Year")}"
            );
        }

        private IActionResult LoadUsageData(
            Func<dynamic, bool> filter,
            string title)
        {
            var data =
                (from wo in _context.WorkOrder

                 join dp in _context.DemandProduction
                    on wo.DemandId equals dp.DemandId

                 join pm in _context.ProductMaster
                    on wo.ProjectName equals pm.ProjectName

                 join mo in _context.MaterialOrder
                    on wo.WoNumber equals mo.WO
                    into materialGroup

                 from mo in materialGroup.DefaultIfEmpty()

                 select new
                 {
                     WO = wo.WoNumber,
                     Project = wo.ProjectName,
                     Qty = dp.Quantity,
                     WOStart = dp.WOStart,
                     SPPN = pm.SPPN,
                     CPN = pm.CPN,

                     VolumePerPcs =
                        Convert.ToDouble(pm.VolumePerPcs),

                     SolderPastePerPcs =
                        Convert.ToDouble(pm.SolderPastePerPcs),

                     TotalVolume =
                        dp.Quantity *
                        Convert.ToDouble(pm.VolumePerPcs),

                     QtyJar =
                        (int)Math.Ceiling(
                            (
                                dp.Quantity *
                                Convert.ToDouble(pm.SolderPastePerPcs)
                            ) / 500.0
                        ),

                     TransferStatus =
                        mo != null
                        ? mo.TransferStatus
                        : "Pending"
                 })

                 .AsEnumerable()
                 .Where(filter)
                 .ToList();

            int totalJar = 0;
            double totalVolume = 0;

            foreach (var item in data)
            {
                totalJar += Convert.ToInt32(item.QtyJar);

                totalVolume += Convert.ToDouble(
                    item.TotalVolume
                );
            }

            double solderUsage =
                totalJar * 500.0;

            ViewBag.Title = title;

            ViewBag.TotalSKID =
                totalJar;

            ViewBag.TotalJar =
                totalJar;

            ViewBag.SolderUsage =
                solderUsage;

            ViewBag.TotalVolume =
                Math.Round(
                    totalVolume,
                    2
                );

            ViewBag.UsageRate =
                totalVolume == 0
                    ? 0
                    : Math.Round(
                        solderUsage / totalVolume,
                        2
                    );

            return View(
                "UsageView",
                data
            );
        }
    }
}