namespace SolderPasteUsage.Models
{
    public class DashboardSummary
    {
        public int TotalSkid { get; set; }

        public int TotalJar { get; set; }

        public double TotalVolume { get; set; }

        public double UsageRate { get; set; }

        public List<DashboardDetail> Details { get; set; }
            = new List<DashboardDetail>();
    }

    public class DashboardDetail
    {
        public string WO { get; set; } = "";

        public string ProjectName { get; set; } = "";

        public string SPPN { get; set; } = "";

        public string CPN { get; set; } = "";

        public int Qty { get; set; }

        public double VolumePerPcs { get; set; }

        public double SolderPastePerPcs { get; set; }

        public double TotalVolume { get; set; }

        public int QtyJar { get; set; }

        public string TransferStatus { get; set; } = "";
    }
}