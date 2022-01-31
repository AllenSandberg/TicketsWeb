
namespace DataObjects.Tickets.TicketUpdate
{
    /** Input parameter for TicketsUpdate(TicketUpdateParameters parameters) */
    public class TicketUpdateParameters
    {
        public string SessionId { get; set; }
        public string StatusName { get; set; }
        public int TicketId { get; set; }
        public string LastComment { get; set; }
    }
}
