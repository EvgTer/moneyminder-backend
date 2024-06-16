using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMinder.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }

        [Required]
        public required int UserID { get; set; }

        [ForeignKey("UserID")]
        public User ?User { get; set; }

        [Required]
        public required int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public TransactionCategory? Category { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public required decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        [StringLength(255)]
        public string ?Description { get; set; }

        [StringLength(3)]
        public string? Currency { get; set; }
    }

}
