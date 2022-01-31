namespace DataObjects.Tickets.TicketDocumentFetch
{

    /** Input Parameter for TicketDocumentFetch(TicketDocumentFetchParameters parameters) */
    public class TicketDocumentFetchParameters
    {
        public string SessionId { set; get; }
        public int TicketId { set; get; }
        public int DocumentId { set; get; }
    }
}
