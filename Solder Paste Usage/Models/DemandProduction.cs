using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolderPasteUsage.Models
{
    [Table("DemandProduction")]
    public class DemandProduction
    {
        [Key]
        public int DemandId { get; set; }

        public string Line { get; set; } = "";

        public string WO { get; set; } = "";

        public string ProjectName { get; set; } = "";

        public string Family { get; set; } = "";

        public int Quantity { get; set; }

        public decimal UPH { get; set; }

        public DateTime WOStart { get; set; }
    }
}