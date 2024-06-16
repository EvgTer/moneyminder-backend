using System.ComponentModel.DataAnnotations;

namespace MoneyMinder.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public required string Username { get; set; }

        [Required]
        [StringLength(100)]
        public required string PasswordHash { get; set; }

        [Required]
        [StringLength(100)]
        public required string Email { get; set; }

        public DateTime CreateDate { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public ICollection<Admin> Admins { get; set; } = new List<Admin>();
        public ICollection<AdminUserRelation> AdminUserRelations { get; set; } = new List<AdminUserRelation>();
    }
}