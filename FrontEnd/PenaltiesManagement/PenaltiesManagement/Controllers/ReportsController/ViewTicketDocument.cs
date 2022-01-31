using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace PenaltiesManagement.Controllers.ReportsController
{
    public partial class ReportsController : BaseController
    {
        [HttpGet]
        [Route("Reports/ViewTicketDocument/{ticketId}/{documentId}")]
        public async Task<FileContentResult> ViewTicketDocument(int ticketId, int documentId)
        {
            var result = await _ticketsApiService.TicketDocumentFetch(new Models.API.TicketsApi.TicketDocumentFetch.TicketDocumentFetchApiRequest
            {
                SessionId = SessionId,
                DocumentId=documentId,
                TicketId=ticketId,
            });
            if(result.FileName!=null && result.FileName.Length>0)
                Response.Headers.Add("content-disposition", "attachment; filename=" + result.FileName);
            else
                Response.Headers.Add("content-disposition", "attachment; filename=doc" + result.DocumentType + ".jpg");
            byte[] byteArray = Convert.FromBase64String(result.DocumentDataBase64);

            return new FileContentResult(byteArray, "application/octet-stream");
        }

    }
}
