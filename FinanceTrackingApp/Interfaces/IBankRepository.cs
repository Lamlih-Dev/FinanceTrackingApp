using FinanceTrackingApp.Models;

namespace FinanceTrackingApp.Interfaces
{
    public interface IBankRepository
    {
        ICollection<Bank> GetBanks();
        Bank GetBank(int id);
        ICollection<BankAccount> GetBankAccounts(int id);
        ICollection<User> GetBankUsers(int id);
        bool BankExists(string name);
        bool BankExists(int id);
        bool CreateBank(Bank newBank);
        bool UpdateBank(Bank newBank);
        bool DeleteBank(Bank Bank);
        bool Save();
    }
}
