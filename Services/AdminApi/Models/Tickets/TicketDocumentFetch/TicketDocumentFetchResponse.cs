namespace AdminApi.Models.Tickets.TicketDocumentFetch
{
    public class TicketDocumentFetchResponse:BaseResponse
    {
        public string DocumentDateBase64 { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public byte[] DocumentDataBase64Binary { get; set; }
    }
}
