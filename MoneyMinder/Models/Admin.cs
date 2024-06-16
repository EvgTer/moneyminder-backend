using System.ComponentModel.DataAnnotations;

namespace MoneyMinder.Models
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }

        [Required]
        [StringLength(50)]
        public required string Username { get; set; }

        [Required]
        [StringLength(100)]
        public required string PasswordHash { get; set; }

        [Required]
        [StringLength(100)]
        public required string Email { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();

        public ICollection<AdminUserRelation> AdminUserRelations { get; set; } = new List<AdminUserRelation>();
    }
}
