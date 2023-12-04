namespace FinanceTrackingApp.Dto
{
    public class BankAccountDto
    {
        public int BankAccountID { get; set; }
        public int BankID { get; set; }
        public int UserID { get; set; }
        public double BankAccountBalance { get; set; }
        public int BankAccountRIB { get; set; }
    }
}
