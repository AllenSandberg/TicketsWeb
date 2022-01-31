using PenaltiesManagement.Models.Entities;
using System.Collections.Generic;

namespace PenaltiesManagement.ViewModel
{
    public class TicketDetailsViewModel
    {
        public TicketEntity TicketDetails { get; set; }
        public List<StatusEntity> Statuses { get; set; }
        public List<TicketDocumentEntity> Documents { set; get; }
        public List<DocumentType> DocumentTypes { set; get; }
    }
}
