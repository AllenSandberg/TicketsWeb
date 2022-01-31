namespace PenaltiesManagement.Models.API.TicketsApi.TicketDocumentFetch
{
    public class TicketDocumentFetchApiResponse : BaseApiResponse
    {
        public string DocumentDataBase64 { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public byte[] DocumentDataBase64Binary { get; set; }
    }
}
