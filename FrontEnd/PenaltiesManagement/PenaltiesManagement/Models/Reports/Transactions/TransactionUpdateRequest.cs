
namespace PenaltiesManagement.Models.Reports.Transactions
{
    public class TransactionUpdateRequest
    {
        public int TransactionId { get; set; }
        public int Status { get; set; }
        public string BankReferenceNumber { get; set; }
        public decimal SettledAmount { get; set; }
    }
}
