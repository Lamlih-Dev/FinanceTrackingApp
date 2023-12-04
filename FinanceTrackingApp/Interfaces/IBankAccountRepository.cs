using FinanceTrackingApp.Models;

namespace FinanceTrackingApp.Interfaces
{
    public interface IBankAccountRepository
    {
        ICollection<BankAccount> GetBankAccounts();
        BankAccount GetBankAccount(int id);
        bool BankAccountExists(int id);
        bool CreateBankAccount(BankAccount newBankAccount);
        bool UpdateBankAccount(BankAccount newBankAccount);
        bool DeleteBankAccount(BankAccount BankAccount);
        bool Save();
    }
}
