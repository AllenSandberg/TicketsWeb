using AdminApi.Models.Entities;
using System.Collections.Generic;

namespace AdminApi.Models.Tickets
{
    public class TicketsListResponse:BaseResponse
    {
        public List<TicketEntiity> Tickets { set; get; }
    }
}
