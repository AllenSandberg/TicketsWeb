using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjects.Tickets.TicketDocumentsList
{
    /** Returned Result for TicketDocumentsList(TicketDocumentsListParameters parameters) */
    public class TicketDocumentsListResult:BaseResult
    {
        public List<TicketDocument> TicketDocuments { get; set; }
    }

    public class TicketDocument
    {
        public int DocumentId { set; get; }
        public string DocumentType { set; get; }
        public DateTime Created { set; get; }
        public DateTime Updated { set; get; }
        public string FileName { get; set; }
    }
}
