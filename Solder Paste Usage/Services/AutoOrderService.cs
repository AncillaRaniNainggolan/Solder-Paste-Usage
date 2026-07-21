using SolderPasteUsage.Data;
using SolderPasteUsage.Models;

namespace SolderPasteUsage.Services
{
    public class AutoOrderService
    {
        private readonly ApplicationDbContext _context;

        public AutoOrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void GenerateMaterialOrder()
        {
            var workOrders =
                (from wo in _context.WorkOrder
                 join dp in _context.DemandProduction
                    on wo.DemandId equals dp.DemandId
                 join pm in _context.ProductMaster
                    on wo.ProjectName equals pm.ProjectName
                 select new
                 {
                     wo.WoNumber,
                     wo.ProjectName,
                     dp.Quantity,
                     pm.SPPN,
                     pm.CPN,
                     pm.SolderPastePerPcs
                 })
                 .ToList();

            foreach (var item in workOrders)
            {
                bool exists =
                    _context.MaterialOrder
                    .Any(x => x.WO == item.WoNumber);

                if (exists)
                    continue;

                int qtyJar =
                    (int)Math.Ceiling(
                        (
                            item.Quantity *
                            Convert.ToDouble(
                                item.SolderPastePerPcs
                            )
                        ) / 500.0
                    );

                _context.MaterialOrder.Add(
                    new MaterialOrder
                    {
                        Line = "Line01",

                        WO = item.WoNumber,

                        ProjectName =
                            item.ProjectName,

                        SPPN =
                            item.SPPN,

                        CPN =
                            item.CPN,

                        QtyJar =
                            qtyJar,

                        TransferStatus =
                            "Order Generated",

                        OrderDate =
                            DateTime.Now,

                        AutoOrderTime =
                            DateTime.Now
                    });
            }

            _context.SaveChanges();
        }
    }
}