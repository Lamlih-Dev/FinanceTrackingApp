using FinanceTrackingApp.Data;
using FinanceTrackingApp.Interfaces;
using FinanceTrackingApp.Models;

namespace FinanceTrackingApp.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _context;

        public TransactionRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateTransaction(Transaction newTransaction, int CategoryID, int TypeID, int UserID)
        {
            newTransaction.Category = _context.Categories.Where(c => c.CategoryID == CategoryID).FirstOrDefault();
            newTransaction.Type = _context.Types.Where(t => t.TypeID == TypeID).FirstOrDefault();
            newTransaction.User = _context.Users.Where(u => u.UserID == UserID).FirstOrDefault();

            _context.Add(newTransaction);
            return Save();
        }

        public bool UpdateTransaction(Transaction newTransaction, int CategoryID, int TypeID, int UserID)
        {
            newTransaction.Category = _context.Categories.Where(c => c.CategoryID == CategoryID).FirstOrDefault();
            newTransaction.Type = _context.Types.Where(t => t.TypeID == TypeID).FirstOrDefault();
            newTransaction.User = _context.Users.Where(u => u.UserID == UserID).FirstOrDefault();

            _context.Update(newTransaction);
            return Save();
        }

        public bool DeleteTransaction(Transaction Transaction)
        {
            _context.Remove(Transaction);
            return Save();
        }

        public Transaction GetTransaction(int id)
        {
            return _context.Transactions.Where(t => t.TransactionID == id).FirstOrDefault();
        }

        public ICollection<Transaction> GetTransactions()
        {
            return _context.Transactions.ToList();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool TransactionExists(int id)
        {
            return _context.Transactions.Any(t => t.TransactionID == id);
        }public bool TransactionExists(string name)
        {
            return _context.Transactions.Any(t => t.TransactionName.Trim().ToLower() == name.Trim().ToLower());
        }
    }
}
