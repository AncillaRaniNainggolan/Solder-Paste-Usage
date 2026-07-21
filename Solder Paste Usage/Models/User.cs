using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SolderPasteUsage.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; } = "";

        public string PasswordHash { get; set; } = "";

        public string Role { get; set; } = "";
    }
}