using AdminApi.Models.Entities;
using System.Collections.Generic;

namespace AdminApi.Models.Tickets
{
    /** Input: From FrontEnd - Filtering Options for TicketList  */
    public class TicketsListRequest
    {
        public string SessionId { get; set; }
        public string Passport { get; set; }
        public string VichilePlateNumber { get; set; }
        public int Ticketid { get; set; }
        public string PenaltyReportNumber { get; set; }
        public string Phone { get; set; }
    }
}
