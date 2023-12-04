using FinanceTrackingApp.Data;
using FinanceTrackingApp.Interfaces;
using FinanceTrackingApp.Models;

namespace FinanceTrackingApp.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly DataContext _context;

        public BankRepository(DataContext context)
        {
            _context = context;
        }

        public bool BankExists(string name)
        {
            return _context.Banks.Any(b => b.BankName.Trim().ToLower() == name.Trim().ToLower());
        }

        public bool BankExists(int id)
        {
            return _context.Banks.Any(b => b.BankID == id);
        }

        public ICollection<Bank> GetBanks()
        {
            return _context.Banks.ToList();
        }
        public Bank GetBank(int id)
        {
            return _context.Banks.Where(b => b.BankID == id).FirstOrDefault();
        }

        public ICollection<BankAccount> GetBankAccounts(int id)
        {
            return _context.BankAccounts.Where(ba => ba.Bank.BankID == id).ToList();
        }

        public ICollection<User> GetBankUsers(int id)
        {
            return _context.BankAccounts.Where(ba => ba.Bank.BankID == id).Select(ba => ba.User).ToList();
        }

        public bool CreateBank(Bank newBank)
        {
            _context.Add(newBank);
            return Save();
        }

        public bool UpdateBank(Bank newBank)
        {
            _context.Update(newBank);
            return Save();
        }

        public bool DeleteBank(Bank Bank)
        {
            _context.Remove(Bank);
            return Save();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }
    }
}
