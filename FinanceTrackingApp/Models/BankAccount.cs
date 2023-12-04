using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTrackingApp.Models
{
    public class BankAccount
    {
        [Key]
        public int BankAccountID { get; set; }
        public double BankAccountBalance { get; set; }
        public int BankAccountRIB { get; set; }

        [ForeignKey("Bank")]
        public int BankID { get; set; }
        public Bank Bank { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
