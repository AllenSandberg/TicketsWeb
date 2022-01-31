namespace DataObjects.Tickets.TicketsList
{
    /** Input Parameter for TicketsList(TicketDocumentsListParameters parameters) */
    public class TicketDocumentsListParameters
    {
        public string SessionId { get; set; }
        public string Passport { get; set; }
        public string VichilePlateNumber { get; set; }
        public string PenaltyReportNumber { get; set; }
        public string Phone { get; set; }
        public int TicketId { get; set; }
    }
}
