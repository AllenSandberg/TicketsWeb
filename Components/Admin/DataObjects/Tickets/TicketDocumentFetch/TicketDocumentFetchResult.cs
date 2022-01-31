namespace DataObjects.Tickets.TicketDocumentFetch
{
    /** Returned Result for TicketDocumentFetch(TicketDocumentFetchParameters parameters) */

    public class TicketDocumentFetchResult:BaseResult
    {
        public string DocumentDataBase64 { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
    }
}
