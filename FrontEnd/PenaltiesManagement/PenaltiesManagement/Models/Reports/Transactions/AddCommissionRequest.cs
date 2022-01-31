namespace PenaltiesManagement.Models.Reports.Transactions
{
    public class AddCommissionRequest
    {
        public string Currency { get; set; }
        public double Amount { get; set; }
        public int TransactionType { get; set; }
    }
}
