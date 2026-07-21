using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolderPasteUsage.Models
{
    [Table("ProductMaster")]
    public class ProductMaster
    {
        [Key]
        public int ProductId { get; set; }

        public string ProjectName { get; set; } = "";

        public string SPPN { get; set; } = "";

        public string CPN { get; set; } = "";

        public decimal VolumePerPcs { get; set; }

        public decimal SolderPastePerPcs { get; set; }
    }
}