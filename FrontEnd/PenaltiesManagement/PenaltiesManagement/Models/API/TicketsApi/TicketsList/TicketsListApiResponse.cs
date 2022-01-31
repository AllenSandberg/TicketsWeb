using PenaltiesManagement.Models.Entities;
using System.Collections.Generic;

namespace PenaltiesManagement.Models.API.TicketsApi.TicketsList
{
    public class TicketsListApiResponse: BaseApiResponse
    {
        public List<TicketEntity> Tickets { get; set; }
    }
}
