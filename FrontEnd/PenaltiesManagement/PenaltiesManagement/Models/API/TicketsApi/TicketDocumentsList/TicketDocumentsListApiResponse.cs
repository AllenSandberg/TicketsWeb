
using PenaltiesManagement.Models.Entities;
using System.Collections.Generic;

namespace PenaltiesManagement.Models.API.TicketsApi.TicketDocumentsList
{
    public class TicketDocumentsListApiResponse:BaseApiResponse
    {
        public List<TicketDocumentEntity> TicketDocuments { get; set; }
    }

}
