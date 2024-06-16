using System.Collections.Generic;
using MoneyMinder.Models;

namespace MoneyMinder.Interfaces
{
    public interface ITransactionCategoryRepository
    {
        ICollection<TransactionCategory> GetTransactionCategories();
        TransactionCategory GetTransactionCategory(int id);
        TransactionCategory GetTransactionCategory(string categoryName);
        bool TransactionCategoryExists(int id);
        bool CreateTransactionCategory(TransactionCategory category);
        bool UpdateTransactionCategory(TransactionCategory category);
        bool DeleteTransactionCategory(TransactionCategory category);
        bool Save();
    }
}