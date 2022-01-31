namespace AdminApi.Models.Tickets.TicketDocumentAdd
{
    public class TicketDocumentAddRequest
    {
        public string SessionId { get; set; }
        public string FileName { get; set; }
        public byte[] DocumentData { get; set; }
        public string DocumentType { get; set; }
        public int TicketId { get; set; }
    }
}
