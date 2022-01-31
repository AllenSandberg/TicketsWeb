using DataObjects.Entities;
using System.Collections.Generic;


namespace DataObjects.Tickets.TicketsList
{
    /** Returned Result for TicketsList(TicketDocumentsListParameters parameters) */
    public class TicketsListResult:BaseResult
    {
        public List<Ticket> Tickets { get; set; }
    }
}
