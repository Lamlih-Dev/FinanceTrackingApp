using FinanceTrackingApp.Models;

namespace FinanceTrackingApp.Interfaces
{
    public interface ITransactionRepository
    {
        ICollection<Transaction> GetTransactions();
        Transaction GetTransaction(int id);
        bool TransactionExists(int id);
        bool TransactionExists(string name);
        bool CreateTransaction(Transaction newTransaction, int CategoryID, int TypeID, int UserID);
        bool UpdateTransaction(Transaction newTransaction, int CategoryID, int TypeID, int UserID);
        bool DeleteTransaction(Transaction Transaction);
        bool Save();
    }
}
