using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolderPasteUsage.Models
{

    [Table("MaterialOrder")]
    public class MaterialOrder
    {
        [Key]
        public int OrderId { get; set; }

        public string Line { get; set; } = "";

        public string WO { get; set; } = "";

        public string ProjectName { get; set; } = "";

        public string SPPN { get; set; } = "";

        public string CPN { get; set; } = "";

        public int QtyJar { get; set; }

        public string TransferStatus { get; set; } = "";

        public DateTime OrderDate { get; set; }

        public DateTime AutoOrderTime { get; set; }
    }
}