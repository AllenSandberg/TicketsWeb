using AdminApi.Models.Tickets;
using AdminApi.Models.Tickets.TicketDocumentAdd;
using AdminApi.Models.Tickets.TicketDocumentFetch;
using AdminApi.Models.Tickets.TicketDocumentsList;
using AdminApi.Models.Tickets.TicketUpdate;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AdminApi.Controllers
{
    public class TicketsController: BaseController
    {
        /** Returned Result per Request - Specific TicketDocumentDetails With BLOB. */
        [HttpPost]
        [Route("document/data")]
        public IActionResult TicketDocumentFetch([FromBody] TicketDocumentFetchRequest request)
        {
            try
            {
                var result = _ticketsManager.TicketDocumentFetch(new DataObjects.Tickets.TicketDocumentFetch.TicketDocumentFetchParameters
                {
                    SessionId = request.SessionId,
                    TicketId = request.TicketId,
                    DocumentId=request.DocumentId
                });

                return Json(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /** Provide SessionID & TicketID from FrontEnd.
         *  Response: A a list of a single document object - (id,imagetype,updatedAt,createdAt) .
         *  Wrapped in JSON to the FrontEnd - for viewing purposes.*/
 
        [HttpPost]
        [Route("document/list")]
        public IActionResult TicketDocumentsList([FromBody] TicketDocumentsListRequest request)
        {
            try
            {
                var result = _ticketsManager.TicketDocumentsList(new DataObjects.Tickets.TicketDocumentsList.TicketDocumentsListParameters
                {
                    SessionId = request.SessionId,
                    TicketId = request.TicketId,
                });

                return Json(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /** Input: From FrontEnd - Filtering Options for TicketList 
            Output : JSON Object wrapped TicketList [AllTickets for no filtering/ Specific Tickets per filtering]*/
        [HttpPost]
        [Route("list")]
        public IActionResult TicketsList([FromBody] TicketsListRequest request)
        {
            try
            {
                var result = _ticketsManager.TicketsList(new DataObjects.Tickets.TicketsList.TicketDocumentsListParameters
                {
                    SessionId = request.SessionId,
                    Passport=request.Passport,
                    Phone=request.Phone,
                    TicketId=request.Ticketid,
                    PenaltyReportNumber=request.PenaltyReportNumber,
                    VichilePlateNumber=request.VichilePlateNumber
                });

                return Json(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /** Input from FrontEnd: TicketUpdateRequest Specific details for update.
         *  Response: JSON Object Indicating Successful Update Operation. */
        [HttpPost]
        [Route("update")]
        public IActionResult TicketUpdate([FromBody] TicketUpdateRequest request)
        {
            try
            {
                var result = _ticketsManager.TicketUpdate(new DataObjects.Tickets.TicketUpdate.TicketUpdateParameters
                {
                    SessionId = request.SessionId,
                    StatusName=request.Status,
                    TicketId=request.TicketId,
                    LastComment=request.LastComment
                });

                return Json(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        [Route("document/add")]
        public IActionResult TicketDocumentAdd([FromBody] TicketDocumentAddRequest request)
        {
            try
            {
                var result = _ticketsManager.TicketDocumentAdd(new DataObjects.Tickets.TicketDocumentAdd.TicketDocumentAddParameters
                {
                    SessionId = request.SessionId,
                    DocumentData=request.DocumentData,
                    DocumentType=request.DocumentType,
                    FileName=request.FileName,
                    TicketId=request.TicketId
                });

                return Json(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
