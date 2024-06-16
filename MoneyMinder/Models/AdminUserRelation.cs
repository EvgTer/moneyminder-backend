using System.ComponentModel.DataAnnotations;

namespace MoneyMinder.Models
{
    public class AdminUserRelation
    {
        public required int AdminID { get; set; }

        public required int UserID { get; set; }

        public Admin ?Admin { get; set; }

        public User ?User { get; set; }
    }
}