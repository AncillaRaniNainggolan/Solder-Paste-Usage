using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolderPasteUsage.Models
{
    [Table("StencilVolumePerCavity")]
    public class StencilVolumePerCavity
    {
        [Key]
        public int Id { get; set; }

        public string ProjectName { get; set; } = "";

        public double Volume { get; set; }

        public double VolumeThicknessPerCavity { get; set; }
    }
}