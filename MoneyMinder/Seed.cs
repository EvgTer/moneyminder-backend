using System;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoneyMinder.Helpers.DbClasses;
using MoneyMinder.Models;

namespace MoneyMinder
{
    public class Seed
    {
        private readonly MoneyMinderContext context;

        public Seed(MoneyMinderContext context)
        {
            this.context = context;
        }
        public void SeedDataContext()
        {
            try
            {
                // Проверяем, есть ли уже данные в базе
                if (context != new MoneyMinderContext(null))
                {
                    // Добавляем пользователей
                    context.Users.AddRange(
                        new User { Username = "Alex", PasswordHash = BCrypt.Net.BCrypt.HashPassword("qwerty", 10), Email = "alex@i.ua" },
                        new User { Username = "Frank", PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345678", 10), Email = "frank@gmail.com" }
                    );
                    context.Admins.AddRange(
                        new Admin { Username = "Admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin", 10), Email="admin@i.ua"}
                    );
                
                
                    // Добавляем категории транзакций
                    context.TransactionCategories.AddRange(
                        new TransactionCategory { CategoryName = "Food" },
                        new TransactionCategory { CategoryName = "Shopping" },
                        new TransactionCategory { CategoryName = "Entertainment" }
                    );
                    context.Transactions.AddRange(
                        new Transaction
                        {
                            CategoryID = context.TransactionCategories.FirstOrDefault(c => c.CategoryName == "Food").CategoryID,
                            Amount = -200,
                            UserID = context.Users.FirstOrDefault(c => c.Username == "Alex").UserID,
                            TransactionDate = DateTime.ParseExact("05.08.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture)
                        },
                        new Transaction
                        {
                            CategoryID = context.TransactionCategories.FirstOrDefault(c => c.CategoryName == "Salary").CategoryID,
                            Amount = 200,
                            UserID = context.Users.FirstOrDefault(c => c.Username == "Frank").UserID,
                            TransactionDate = DateTime.Now
                        }
                    );
                    // Сохраняем изменения в базе данных
                    if (context.AdminUserRelations.Any())
                    {
                        foreach (var user in context.Users.ToList())
                        {
                            context.AdminUserRelations.Add(
                            new AdminUserRelation { AdminID = context.Admins.FirstOrDefault(a => a.Email == "admin@i.ua").AdminID, UserID = user.UserID });
                        }
                    }
                }
                // Сохраняем изменения в базе данных
                context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
