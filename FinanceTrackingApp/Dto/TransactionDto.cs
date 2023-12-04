namespace FinanceTrackingApp.Dto
{
    public class TransactionDto
    {
        public int TransactionID { get; set; }
        public string TransactionName { get; set; }
        public string TransactionDescription { get; set; }
        public double TransactionAmout { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
