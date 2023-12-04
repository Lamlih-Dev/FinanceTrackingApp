using System.ComponentModel.DataAnnotations;

namespace FinanceTrackingApp.Models
{
    public class Bank
    {
        [Key]
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string BankCountry { get; set; }
        public string BankCity { get; set; }
        public string BankEmail { get; set; }
        public string BankPhone { get; set; }

        public ICollection<BankAccount> BankAccounts { get; set; }
    }
}
