
namespace PenaltiesManagement.Models.API.TicketsApi.TicketUpdate
{
    public class TicketUpdateApiRequest
    {
        public string SessionId { get; set; }
        public string Status { get; set; }
        public string LastComment { get; set; }
        public int TicketId { get; set; }
    }
}
