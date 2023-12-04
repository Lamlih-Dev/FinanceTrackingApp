using System.ComponentModel.DataAnnotations;

namespace FinanceTrackingApp.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; }
    }
}
