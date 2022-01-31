using DataAccessLayer;
using DataObjects.Entities;
using DataObjects.Tickets.TicketDocumentAdd;
using DataObjects.Tickets.TicketDocumentFetch;
using DataObjects.Tickets.TicketDocumentsList;
using DataObjects.Tickets.TicketsList;
using DataObjects.Tickets.TicketUpdate;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace BuisnessLogic
{
    public class TicketsManager
    {
        #region Fields

        private readonly TicketDal _ticketsDal;

        #endregion

        #region Ctor

        public TicketsManager()
        {
            _ticketsDal = new TicketDal();
        }

        #endregion

        #region Public Methods

        /* Fetch Ticket Document Data [Including Blob] - Per SessionID,TicketID & DocumentID */
        public TicketDocumentFetchResult TicketDocumentFetch(TicketDocumentFetchParameters parameters)
        {
            try
            {
                var result = _ticketsDal.TicketDocumentFetch(parameters);
                return result;
            }
            catch (Exception ex)
            {
                return new TicketDocumentFetchResult
                {
                    ErrorCode = -1000,
                    ErrorDescription = ex.Message
                };
            }
        }
        /* Fetch Ticket Document List Data [Not Including Blob, only ticket details] - Per SessionID & TicketID.
           Based on DocumentList Stored-Procedure.*/

        public TicketDocumentsListResult TicketDocumentsList(DataObjects.Tickets.TicketDocumentsList.TicketDocumentsListParameters parameters)
        {
            try
            {
                var result = _ticketsDal.TicketDocumentsList(parameters);
                return result;
            }
            catch (Exception ex)
            {
                return new TicketDocumentsListResult
                {
                    ErrorCode = -1000,
                    ErrorDescription = ex.Message
                };
            }
        }

        /** Get List of Ticket Results per Session,Passport,VehicleID.
         *  Based on TicketsList Stored-Procedure.*/
        public TicketsListResult TicketsList(DataObjects.Tickets.TicketsList.TicketDocumentsListParameters parameters)
        {
            try
            {
                var result = _ticketsDal.TicketsList(parameters);
                return result;
            }
            catch (Exception ex)
            {
                return new TicketsListResult
                {
                    ErrorCode = -1000,
                    ErrorDescription = ex.Message
                };
            }
        }

        /** Returns Boolean Result (with error if false) - whether TicketUpdate was successful (StatusName/LastComment) */
        public TicketUpdateResult TicketUpdate(TicketUpdateParameters parameters)
        {
            try
            {
                var result = _ticketsDal.TicketsUpdate(parameters);
                // Here we will send the PushMessage with TicketID & StatusName to the Mobile.
                SendPushMessage(parameters.TicketId, parameters.StatusName);
                return result;
            }
            catch (Exception ex)
            {
                return new TicketUpdateResult
                {
                    ErrorCode = -1000,
                    ErrorDescription = ex.Message
                };
            }
        }
        public TicketDocumentAddResult TicketDocumentAdd(TicketDocumentAddParameters parameters)
        {
            try
            {
                var result = _ticketsDal.TicketDocumentAdd(parameters);
                return result;
            }
            catch (Exception ex)
            {
                return new TicketDocumentAddResult
                {
                    ErrorCode = -1000,
                    ErrorDescription = ex.Message
                };
            }
        }

        #endregion
        #region Private Methods
        private void SendPushMessage(int ticketId,string statusName)
        {
            using (var client=new HttpClient())
            {
                // PushMessage - ticketID , TicketStatus , Message
                PushMessage message = new PushMessage
                {
                    id = ticketId,
                    ticket_status = statusName,
                    message="סטטוס חדש - " + statusName
                };
                // Send PushMessage as A PUT Request to the specified URI - Here we get the Result of the PUT Request of JSON [PushMessage String]
                var result=client.PutAsync("http://35.195.169.235:3000/update_ticket_status", new StringContent(JsonConvert.SerializeObject(message).ToString(), Encoding.UTF8, "application/json")).Result;
            }
        }
        #endregion
    }
}
