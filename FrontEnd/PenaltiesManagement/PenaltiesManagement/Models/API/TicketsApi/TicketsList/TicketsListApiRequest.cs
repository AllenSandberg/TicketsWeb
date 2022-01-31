namespace PenaltiesManagement.Models.API.TicketsApi.TicketsList
{
    public class TicketsListApiRequest
    {
        public string SessionId { get; set; }
        public string Passport { get; set; }
        public string VichilePlateNumber { get; set; }
        public string PenaltyReportNumber { get; set; }
        public string Phone { get; set; }
        public int TicketId { get; set; }
    }
}
