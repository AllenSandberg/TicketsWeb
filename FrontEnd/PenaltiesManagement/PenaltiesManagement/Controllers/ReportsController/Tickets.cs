using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using PenaltiesManagement.Models.API.TicketsApi.TicketsList;
using PenaltiesManagement.Models.Reports.Tickets;
using PenaltiesManagement.ViewModel;
using PenaltiesManagement.Models.Entities;
using System.IO;
using PenaltiesManagement.Models.API.TicketsApi.TicketDocumentAdd;
using System.Text;

namespace PenaltiesManagement.Controllers.ReportsController
{
    public partial class ReportsController : BaseController
    {
        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> Tickets([FromQuery]int? pageNumber = 1)
        {
            TicketsListApiResponse result;
            TicketsFilterRequest filterRequest;
            if (HttpContext.Session.GetString("AdminTransactionsSearch") != null && HttpContext.Session.GetString("AdminTransactionsSearch").Length > 0)
            {
                filterRequest = JsonConvert.DeserializeObject<TicketsFilterRequest>(HttpContext.Session.GetString("AdminTransactionsSearch"));
                result = await _ticketsApiService.TicketsList(new TicketsListApiRequest
                {
                    SessionId = SessionId,
                    Passport = filterRequest.Passport,
                    Phone = filterRequest.Phone,
                    PenaltyReportNumber = filterRequest.PenaltyReportNumber,
                    VichilePlateNumber = filterRequest.VechilePlateNumber
                    //PageNumber = filterRequest.PageNumber
                });
            }
            else
            {
                result = await _ticketsApiService.TicketsList(new TicketsListApiRequest
                {
                    SessionId = SessionId,
                    Passport = "",
                    Phone = "",
                    PenaltyReportNumber = "",
                    VichilePlateNumber = ""
                });
                filterRequest = new TicketsFilterRequest { RegisteredFrom = DateTime.Now.AddDays(-7), RegisteredTo = DateTime.Now };
            }
            return View(CreateTicketsListViewModel(filterRequest, result, pageNumber));
        }
        [HttpPost]
        public async Task<IActionResult> Tickets(TicketsFilterRequest request, int? pageNumber = 1)
        {
            HttpContext.Session.SetString("AdminTransactionsSearch", JsonConvert.SerializeObject(request));
            var result = await _ticketsApiService.TicketsList(new TicketsListApiRequest
            {
                Passport = request.Passport,
                Phone = request.Phone,
                //PageNumber = request.PageNumber,
                VichilePlateNumber = request.VechilePlateNumber,
                SessionId = SessionId,
            });
            if (request.hdBtnType == 1)
            {
                Response.Headers.Add("content-disposition", "attachment;filename=TransactionsReport.xls");
                Response.Headers.Add("Content-Type", "application/vnd.ms-excel");
                return View("TransactionsToExcel", CreateTicketsListViewModel(request, result, pageNumber, false));
            }
            else
            {
                return View(CreateTicketsListViewModel(request, result, pageNumber));
            }
        }

        [HttpPost]
        [Route("Reports/TicketDetails/{ticketId}")]
        public async Task<IActionResult> TicketDetails(int ticketId, string status,string lastcomment)
        {
            var result = await _ticketsApiService.TicketUpdate(new Models.API.TicketsApi.TicketUpdate.TicketUpdateApiRequest
            {
                TicketId = ticketId,
                Status=status,
                SessionId = SessionId,
                LastComment= lastcomment
            });

            return Json(result);
        }
        [HttpPost]
        [Route("Reports/Ticket/document/add")]
        public async Task<JsonResult> TicketDocumentAdd(IFormFile file, TicketDocumentAddModel requestModel)
        {
            byte[] fileData;
            if (file != null && file.Length > 0 && requestModel.DocumentType.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    fileData =Encoding.UTF8.GetBytes(Convert.ToBase64String(stream.ToArray()));
                }
                var response = await _ticketsApiService.TicketDocumentAdd(new Models.API.TicketsApi.TicketDocumentAdd.TicketDocumentAddApiRequest
                {
                    SessionId = SessionId,
                    DocumentType = requestModel.DocumentType,
                    FileName = file.FileName,
                    TicketId = requestModel.TicketId,
                    DocumentData=fileData
                });
                return Json(response);
            }

            return Json(new TicketDocumentAddApiResponse {
                ErrorCode=-1000,
                ErrorDescription="בעיה בקובץ"
            }
            );
        }

        [HttpGet]
        [Route("Reports/TicketDetails/{ticketId}")]
        public async Task<IActionResult> TicketDetails(int ticketId)
        {
            var result = await _ticketsApiService.TicketsList(new TicketsListApiRequest
            {
                TicketId = ticketId,
                SessionId = SessionId
            });
            TicketDetailsViewModel viewModel = new TicketDetailsViewModel();
            if (result.Tickets != null)
            {
                viewModel.TicketDetails = new TicketEntity
                {
                    Created = result.Tickets[0].Created,
                    DriverLicense = result.Tickets[0].DriverLicense,
                    FirstName = result.Tickets[0].FirstName,
                    LastName = result.Tickets[0].LastName,
                    Passport = result.Tickets[0].Passport,
                    PenaltyReportNumber = result.Tickets[0].PenaltyReportNumber,
                    Phone = result.Tickets[0].Phone,
                    Status = result.Tickets[0].Status,
                    SystemTicketId = result.Tickets[0].SystemTicketId,
                    Updated = result.Tickets[0].Updated,
                    VehiclePlateNumber = result.Tickets[0].VehiclePlateNumber,
                    VehileType = result.Tickets[0].VehileType,
                    Comments = result.Tickets[0].Comments,
                    LastComment= result.Tickets[0].LastComment,
                    DriverName = result.Tickets[0].DriverName,
                };
            }

            viewModel.Statuses = new List<StatusEntity>();
            var resultstatuses = await _lookupApiService.GetStatuses();

            foreach(var status in resultstatuses)
            {
                StatusEntity newstatus = new StatusEntity
                {
                    Description = status.Description,
                    StatusId = status.StatusId,
                    StatusName = status.StatusName
                };
                viewModel.Statuses.Add(newstatus);
            }

            viewModel.Documents = new List<TicketDocumentEntity>();
            var resultDocuments = await _ticketsApiService.TicketDocumentsList(new Models.API.TicketsApi.TicketDocumentsList.TicketDocumentsListApiRequest
            {
                TicketId = ticketId,
                SessionId = SessionId
            });
            foreach(var document in resultDocuments.TicketDocuments)
            {
                TicketDocumentEntity newDocument = new TicketDocumentEntity
                {
                    Created= document.Created,
                    DocumentId=document.DocumentId,
                    DocumentType=document.DocumentType,
                    Updated=document.Updated,
                    FileName=document.FileName
                };
                viewModel.Documents.Add(newDocument);
            }

            viewModel.DocumentTypes = new List<DocumentType>();
            var resultDocumentTypes = await _lookupApiService.ClientDocumentTypes();
            foreach(var documentYpe in resultDocumentTypes)
            {
                DocumentType newType = new DocumentType
                {
                    DocumentTypeId=documentYpe.DocumentTypeId,
                    DocumentTypeName=documentYpe.DocumentTypeName
                };
                viewModel.DocumentTypes.Add(newType);
            }

            return View(viewModel);
        }


        #endregion
        #region Private Methods

        /** Generate From TicketListAPIResponse - List<Tickets>  a list of ticket entities.
         *  Pass this list along with Paging & TicketSearchFilter to TicketsListViewModel
            For Specific Display of Tickets (In the specified page, with specified filter parameters)*/
        private TicketsListViewModel CreateTicketsListViewModel(TicketsFilterRequest request, TicketsListApiResponse apiResponse, int? pageNumber, bool usePaging = true)
        {
            var pager = new PagerEntity(apiResponse.Tickets.Count, pageNumber);

            List<TicketEntity> ticketsList = new List<TicketEntity>();
            foreach (var ticket in apiResponse.Tickets)
            {
                ticketsList.Add(new TicketEntity
                {
                    Created = ticket.Created,
                    DriverLicense = ticket.DriverLicense,
                    FirstName = ticket.FirstName,
                    LastName = ticket.LastName,
                    Passport = ticket.Passport,
                    PenaltyReportNumber = ticket.PenaltyReportNumber,
                    Phone = ticket.Phone,
                    Status = ticket.Status,
                    SystemTicketId = ticket.SystemTicketId,
                    Updated = ticket.Updated,
                    VehiclePlateNumber = ticket.VehiclePlateNumber,
                    VehileType = ticket.VehileType,
                    DriverName=ticket.DriverName,
                });
            }
            return new TicketsListViewModel
            {
                Paging = pager,
                Tickets = usePaging ? ticketsList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList() : ticketsList,
                TicketsSearch = new TicketsSearchEntity
                {
                    Passport = request.Passport,
                    Phone = request.Phone,
                    RegisteredFrom = request.RegisteredFrom,
                    RegisteredTo = request.RegisteredTo,
                    VechilePlateNumber = request.VechilePlateNumber,
                    //PageNumber = request.PageNumber,
                }
            };
        }
        #endregion

    }
}
