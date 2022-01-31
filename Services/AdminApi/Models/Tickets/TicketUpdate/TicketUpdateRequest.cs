
namespace AdminApi.Models.Tickets.TicketUpdate
{
    /** Incoming Request from FrontEnd with specific Ticket Fields to be Updated. */
    public class TicketUpdateRequest
    {
        public string SessionId { set; get; }
        public int TicketId { set; get; }
        public string Status { set; get; }
        public string LastComment { set; get; }
    }
}
