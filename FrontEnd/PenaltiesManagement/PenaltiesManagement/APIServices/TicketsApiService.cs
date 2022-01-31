using Newtonsoft.Json;
using PenaltiesManagement.APIServices;
using PenaltiesManagement.Models.API.TicketsApi.TicketDocumentAdd;
using PenaltiesManagement.Models.API.TicketsApi.TicketDocumentFetch;
using PenaltiesManagement.Models.API.TicketsApi.TicketDocumentsList;
using PenaltiesManagement.Models.API.TicketsApi.TicketsList;
using PenaltiesManagement.Models.API.TicketsApi.TicketUpdate;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminApi.APIServices
{
    public class TicketsApiService : BaseApiClient
    {
        /** Retrieve TicketDocumentDetails with Blob - Send a Request to Web-API Server [SessionID, TicketID , DocumentID].
       *  Recieve from WebAPI the response - TicketDocumentType, with Blob (ticket info) .*/

        public async Task<TicketDocumentFetchApiResponse> TicketDocumentFetch(TicketDocumentFetchApiRequest request)
        {
            var result = new TicketDocumentFetchApiResponse();
            try
            {
                var response = await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/tickets/document/data", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TicketDocumentFetchApiResponse>(content);
                }
                if (response.ReasonPhrase == "Bad Request")
                {
                    result = new TicketDocumentFetchApiResponse
                    {
                        ErrorCode = -1000,
                        ErrorDescription = "Genera Error"
                    };
                }
            }
            catch (Exception ex)
            {
                result = new TicketDocumentFetchApiResponse
                {
                    ErrorCode = -1000,
                    ErrorDescription = "Genera Error: " + ex.Message
                };
            }
            return result;
        }

        /** Retrieve TicketDocumentList mismahim- Send a Request to Web-API Server [SessionID, TicketID].
     *  Recieve from WebAPI the response -List of TicketDocument Details .*/

        public async Task<TicketDocumentsListApiResponse> TicketDocumentsList(TicketDocumentsListApiRequest request)
        {
            var result = new TicketDocumentsListApiResponse();
            try
            {
                var response = await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/tickets/document/list", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TicketDocumentsListApiResponse>(content);
                }
                if (response.ReasonPhrase == "Bad Request")
                {
                    result = new TicketDocumentsListApiResponse
                    {
                        ErrorCode = -1000,
                        ErrorDescription = "Genera Error"
                    };
                }
            }
            catch (Exception ex)
            {
                result = new TicketDocumentsListApiResponse
                {
                    ErrorCode = -1000,
                    ErrorDescription = "Genera Error: " + ex.Message
                };
            }
            return result;
        }

        /** Retrieve Specific Ticket - Send a Request to Web-API Server with specific Ticket details to be searched.
     *  Recieve from WebAPI the response -List<Tickets> with all Users' Specific Tickets .*/

        public async Task<TicketsListApiResponse> TicketsList(TicketsListApiRequest request)
        {
            var result = new TicketsListApiResponse();
            try
            {
                var response = await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/tickets/list", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TicketsListApiResponse>(content);
                }
                if (response.ReasonPhrase == "Bad Request")
                {
                    result = new TicketsListApiResponse
                    {
                        ErrorCode = -1000,
                        ErrorDescription = "Genera Error"
                    };
                }
            }
            catch (Exception ex)
            {
                result = new TicketsListApiResponse
                {
                    ErrorCode = -1000,
                    ErrorDescription = "Genera Error: " + ex.Message
                };
            }
            return result;
        }

        /** Send Specific Ticket update details - Send a Request to Web-API Server with specific Ticket details
         * to be updated (Comments & Status).
         *  Recieve from WebAPI the response - Successful Ticket Update Operation.*/

        public async Task<TicketUpdateApiResponse> TicketUpdate(TicketUpdateApiRequest request)
        {
            var result = new TicketUpdateApiResponse();
            try
            {
                var response = await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/tickets/update", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TicketUpdateApiResponse>(content);
                }
                if (response.ReasonPhrase == "Bad Request")
                {
                    result = new TicketUpdateApiResponse
                    {
                        ErrorCode = -1000,
                        ErrorDescription = "Genera Error"
                    };
                }
            }
            catch (Exception ex)
            {
                result = new TicketUpdateApiResponse
                {
                    ErrorCode = -1000,
                    ErrorDescription = "Genera Error: " + ex.Message
                };
            }
            return result;
        }
        public async Task<TicketDocumentAddApiResponse> TicketDocumentAdd(TicketDocumentAddApiRequest request)
        {
            var result = new TicketDocumentAddApiResponse();
            try
            {
                var response = await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/tickets/document/add", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TicketDocumentAddApiResponse>(content);
                }
                if (response.ReasonPhrase == "Bad Request")
                {
                    result = new TicketDocumentAddApiResponse
                    {
                        ErrorCode = -1000,
                        ErrorDescription = "Genera Error"
                    };
                }
            }
            catch (Exception ex)
            {
                result = new TicketDocumentAddApiResponse
                {
                    ErrorCode = -1000,
                    ErrorDescription = "Genera Error: " + ex.Message
                };
            }
            return result;
        }
    }
}
