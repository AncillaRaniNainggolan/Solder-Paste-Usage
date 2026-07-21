using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolderPasteUsage.Models
{
    [Table("WorkOrder")]
    public class WorkOrder
    {
        [Key]
        public int WoId { get; set; }

        public string WoNumber { get; set; } = "";

        public int DemandId { get; set; }

        public string ProjectName { get; set; } = "";
    }
}