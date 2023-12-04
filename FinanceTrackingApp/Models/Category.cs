using System.ComponentModel.DataAnnotations;

namespace FinanceTrackingApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
