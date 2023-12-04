using FinanceTrackingApp.Data;
using FinanceTrackingApp.Interfaces;
using FinanceTrackingApp.Models;

namespace FinanceTrackingApp.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly DataContext _context;

        public BankAccountRepository(DataContext context)
        {
            _context = context;
        }
        public bool BankAccountExists(int id)
        {
            return _context.BankAccounts.Any(ba => ba.BankAccountID == id);
        }

        public bool CreateBankAccount(BankAccount newBankAccount)
        {
            var userID = newBankAccount.UserID;
            var bankID = newBankAccount.BankID;
            newBankAccount.User = _context.Users.Where(u => u.UserID == userID).FirstOrDefault();
            newBankAccount.Bank = _context.Banks.Where(b => b.BankID == bankID).FirstOrDefault();

            _context.Add(newBankAccount);
            return Save();
        }

        public bool UpdateBankAccount(BankAccount newBankAccount)
        {
            var userID = newBankAccount.UserID;
            var bankID = newBankAccount.BankID;
            newBankAccount.User = _context.Users.Where(u => u.UserID == userID).FirstOrDefault();
            newBankAccount.Bank = _context.Banks.Where(b => b.BankID == bankID).FirstOrDefault();

            _context.Update(newBankAccount);
            return Save();
        }

        public bool DeleteBankAccount(BankAccount BankAccount)
        {
            _context.Remove(BankAccount);
            return Save();
        }

        public BankAccount GetBankAccount(int id)
        {
            return _context.BankAccounts.Where(ba => ba.BankAccountID == id).FirstOrDefault();
        }

        public ICollection<BankAccount> GetBankAccounts()
        {
            return _context.BankAccounts.ToList();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }
    }
}
