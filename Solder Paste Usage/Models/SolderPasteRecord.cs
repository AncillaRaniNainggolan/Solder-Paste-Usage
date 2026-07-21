using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolderPasteUsage.Models
{
    [Table("SolderPasteUsage")]
    public class SolderPasteRecord
    {
        [Key]
        public int Id { get; set; }

        public string WO { get; set; } = "";

        public string Project { get; set; } = "";

        public string SP_PN { get; set; } = "";

        public string CPN { get; set; } = "";

        public string Skid { get; set; } = "";

        public int Qty { get; set; }

        public decimal VolumePerPcs { get; set; }

        public decimal SPPerPcs { get; set; }

        public DateTime UsageDate { get; set; }
    }
}