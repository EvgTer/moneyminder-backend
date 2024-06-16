using Microsoft.EntityFrameworkCore;
using MoneyMinder.Models;

namespace MoneyMinder.Helpers.DbClasses
{
    public class MoneyMinderContext: DbContext
    {
        public MoneyMinderContext(DbContextOptions<MoneyMinderContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AdminUserRelation> AdminUserRelations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AdminUserRelation>()
                .HasKey(aur => new { aur.AdminID, aur.UserID });

            modelBuilder.Entity<AdminUserRelation>()
                .HasOne(aur => aur.Admin)
                .WithMany(a => a.AdminUserRelations)
                .HasForeignKey(aur => aur.AdminID);

            modelBuilder.Entity<AdminUserRelation>()
                .HasOne(aur => aur.User)
                .WithMany(u => u.AdminUserRelations)
                .HasForeignKey(aur => aur.UserID);
        }
    }
}
