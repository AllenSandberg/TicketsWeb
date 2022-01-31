using System;

namespace PenaltiesManagement.Models.Reports.Transactions
{
    public class TransactionsFilterRequest
    {
        public DateTime TransactionDateFrom { set; get; }
        public DateTime TransactionDateTo { set; get; }
        public string Currency { set; get; }
        public int Status { set; get; }
        public int PageNumber { set; get; }
        public string MerchantName { set; get; }
        public string TransactionReference { set; get; }
        public int TransactionType { set; get; }
        public int hdBtnType { set; get; }
    }
}
