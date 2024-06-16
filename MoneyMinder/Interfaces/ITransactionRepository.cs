using System.Collections.Generic;
using MoneyMinder.Models;

namespace MoneyMinder.Interfaces
{
    public interface ITransactionRepository
    {
        ICollection<Transaction> GetTransactions();
        Transaction GetTransaction(int id);
        bool TransactionExists(int id);
        bool CreateTransaction(Transaction transaction);
        bool UpdateTransaction(Transaction transaction);
        bool DeleteTransaction(Transaction transaction);
        bool Save();
    }
}
