
namespace PenaltiesManagement.Models.API.TicketsApi.TicketDocumentFetch
{
    public class TicketDocumentFetchApiRequest
    {
        public string SessionId { set; get; }
        public int TicketId { set; get; }
        public int DocumentId { set; get; }
    }
}
