using System;

namespace PenaltiesManagement.Models.Entities
{
    public class TicketDocumentEntity
    {
        public int DocumentId { set; get; }
        public string DocumentType { set; get; }
        public DateTime Created { set; get; }
        public DateTime Updated { set; get; }
        public string FileName { get; set; }
    }
}

