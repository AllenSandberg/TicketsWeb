using System;
using System.Collections.Generic;

namespace AdminApi.Models.Tickets.TicketDocumentsList
{
    public class TicketDocumentsListResponse:BaseResponse
    {
        public List<TicketDocument> TicketDocuments { get; set; }
    }
    public class TicketDocument
    {
        public int DocumentId { set; get; }
        public string DocumentType { set; get; }
        public DateTime Created { set; get; }
        public string FileNAme { get; set; }
        public DateTime Updated { set; get; }
    }

}
