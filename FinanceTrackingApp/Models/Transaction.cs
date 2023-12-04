using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTrackingApp.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }
        public string TransactionName { get; set; }
        public string TransactionDescription { get; set; }
        public double TransactionAmout { get; set; }
        public DateTime TransactionDate { get; set; }

        public Category? Category { get; set; }
        public Type Type { get; set; }
        public User User { get; set; }
    }
}
