using System.ComponentModel.DataAnnotations;

namespace FinanceTrackingApp.Models
{
    public class Type
    {
        [Key]
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public string TypeDescription { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
