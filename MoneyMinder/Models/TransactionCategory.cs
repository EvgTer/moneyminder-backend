using System.ComponentModel.DataAnnotations;

namespace MoneyMinder.Models
{
    public class TransactionCategory
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public required string CategoryName { get; set; }
    }
}
