
namespace AdminApi.Models.Tickets.TicketDocumentFetch
{
    /** This request comes from FrontEnd . Request TicketDocument - Provide SessionID, TicketID, DocumentID. */
    public class TicketDocumentFetchRequest
    {
        public string SessionId { get; set; }
        public int TicketId { get; set; }
        public int DocumentId { get; set; }
    }
}
