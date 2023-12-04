using FinanceTrackingApp.Models;

namespace FinanceTrackingApp.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        ICollection<BankAccount> GetUserBankAccounts(int id);
        ICollection<Bank> GetUserBanks(int id);
        ICollection<Transaction> GetUserTransactions(int id);
        bool UserExists(string username);
        bool UserExists(int id);
        bool CreateUser(User newUser);
        bool UpdateUser(User newUser);
        bool DeleteUser(User User);
        bool Save();
    }
}
