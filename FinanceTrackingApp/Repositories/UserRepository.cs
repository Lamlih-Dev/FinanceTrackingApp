using FinanceTrackingApp.Data;
using FinanceTrackingApp.Interfaces;
using FinanceTrackingApp.Models;

namespace FinanceTrackingApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateUser(User newUser)
        {
            _context.Add(newUser);
            return Save();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(p => p.UserID == id).FirstOrDefault();
        }

        public ICollection<BankAccount> GetUserBankAccounts(int id)
        {
            return _context.BankAccounts.Where(ba => ba.User.UserID == id).ToList();
        }

        public ICollection<Bank> GetUserBanks(int id)
        {
            return _context.BankAccounts.Where(ba => ba.User.UserID == id).Select(ba => ba.Bank).ToList();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public ICollection<Transaction> GetUserTransactions(int id)
        {
            return _context.Transactions.Where(t => t.User.UserID == id).ToList();
        }

        public bool UpdateUser(User newUser)
        {
            _context.Update(newUser);
            return Save();
        }

        public bool DeleteUser(User User)
        {
            _context.Remove(User);
            return Save();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UserExists(string username)
        {
            return _context.Users.Any(p => p.Username.Trim().ToLower() == username.Trim().ToLower());
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(p => p.UserID == id);
        }
    }
}
