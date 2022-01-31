using PenaltiesManagement.APIServices;
using PenaltiesManagement.Models.API.AccountApi.AdminUserDetails;
using PenaltiesManagement.Models.API.AccountApi.AdminUsersList;
using PenaltiesManagement.Models.API.AccountApi.AdminUserUpdate;
using PenaltiesManagement.Models.API.AccountApi.Login;
using PenaltiesManagement.Models.API.AccountApi.Registration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PenaltiesManagement.Models.API.AccountApi.SessionCheck;

namespace AdminApi.APIServices
{
    /** This class represents all account operations with WebAPI's AccountController.
     *  You call these methods from FrontEnd's Controller - These methods send requests
        & recieve responses that are passed to the Website's Controller. */
    public class AccountApiService : BaseApiClient
    {
        public SessionCheckApiResponse SessionCheck(SessionCheckApiRequest request)
        {
            var result = new SessionCheckApiResponse();

            try
            {
                // Send a POST Request to AdminAPI-URL : JSON Request [SessionID] - Send to the Browser's Server.
                var response = _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/account/check", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    // Deserialize SessionResultID
                    result = JsonConvert.DeserializeObject<SessionCheckApiResponse>(content);
                }

                // If Negative Response from .NET Server to Client Browser - Return Negative ErrorCode & ErrorDescription
                if (response.ReasonPhrase == "Bad Request")
                {
                    result = new SessionCheckApiResponse
                    {
                        ErrorCode = -1,
                        ErrorDescription = "No session",
                    };
                }

            }
            catch (Exception ex)
            {
                result = new SessionCheckApiResponse
                {
                    ErrorCode = -1000,
                    ErrorDescription = "General Error " + ex.Message
                };

            }
            return result;
        }

        /** Perform Login - Send a LoginRequest to Web-API Server [Email,Password,ClientIP,Devicenumber] */
        public async Task<LoginApiResponse> Login(LoginApiRequest request)
        {
            var result = new LoginApiResponse();

            try
            {
                var response = await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/account/login", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    // The Deserialized Result contains the result object (ErrorCode,ErrorDescription & SessionID)
                    result = JsonConvert.DeserializeObject<LoginApiResponse>(content);
                }
                if (response.ReasonPhrase == "Bad Request")
                {
                    result = new LoginApiResponse
                    {
                        ErrorCode = -1000,
                        ErrorDescription = "General Error",
                        SessionId = result.SessionId
                    };
                }

            }
            catch (Exception ex)
            {
                result = new LoginApiResponse
                {
                    ErrorCode = -1000,
                    ErrorDescription = "General Error " + ex.Message
                };

            }
            return result;
        }

        /** Perform AdminUserUpdate - Send a Request to Web-API Server [SessionID, Fname,LName,Phone,Permissions] */

        public async Task<AdminUserUpdateApiResponse> AdminUserUpdate(AdminUserUpdateApiRequest request)
        {
            var result = new AdminUserUpdateApiResponse();
            try
            {
                // Send Request in JSON
                var response = await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/account/update", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    // result Deserialized is an object that tells whether AdminUserUpdate Operation was Successful.
                    result = JsonConvert.DeserializeObject<AdminUserUpdateApiResponse>(content);
                }
                if (response.ReasonPhrase == "Bad Request")
                {
                    result = new AdminUserUpdateApiResponse
                    {
                        ErrorCode = -1000,
                        ErrorDescription = "General Error"
                    };
                }
            }
            catch (Exception ex)
            {
                result = new AdminUserUpdateApiResponse
                {
                    ErrorCode = -1000,
                    ErrorDescription = "General Error " + ex.Message
                };
            }
            return result;
        }

        /** Perform AdminUserCreate - Send a Request to Web-API Server [SessionID, Fname,LName,Email, Phone,Permissions].
         *  Recieve from WebAPI the response - Successful AdminUserCreate Operation & UserID of new created User.*/

        public async Task<AdminUserCreateApiResponse> AdminUserCreate(AdminUserCreateApiRequest request)
        {
            var result = new AdminUserCreateApiResponse();
            try
            {
                var response = await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/account/create", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<AdminUserCreateApiResponse>(content);
                }
                if (response.ReasonPhrase == "Bad Request")
                {
                    result = new AdminUserCreateApiResponse
                    {
                        ErrorCode = -1000,
                        ErrorDescription = "General Error"
                    };
                }
            }
            catch (Exception ex)
            {
                result = new AdminUserCreateApiResponse
                {
                    ErrorCode = -1000,
                    ErrorDescription = "General Error " + ex.Message
                };
            }
            return result;
        }

        /** Retrieve AdminUsersList - Send a Request to Web-API Server [SessionID].
         *  Recieve from WebAPI the response -List<AdminUsers> with all Users' Details .*/

        public async Task<AdminUsersListApiResponse> AdminUserList(AdminUsersListApiRequest request)
        {
            var result = new AdminUsersListApiResponse();
            try
            {
                var response = await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/account/list", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<AdminUsersListApiResponse>(content);
                }
                if (response.ReasonPhrase == "Bad Request")
                {
                    result = new AdminUsersListApiResponse
                    {
                        ErrorCode = -1000,
                        ErrorDescription = "General Error"
                    };
                }
            }
            catch (Exception ex)
            {
                result = new AdminUsersListApiResponse
                {
                    ErrorCode = -1000,
                    ErrorDescription = "General Error " + ex.Message
                };
            }
            return result;
        }

        /** Retrieve AdminUserDetails - Send a Request to Web-API Server [SessionID,UserID].
     *  Recieve from WebAPI the response - AdminUser per specific UserID with all Users' Details .*/


        public async Task<AdminUserDetailsApiResponse> AdminUserDetails(AdminUserDetailsApiRequest request)
        {
            var result = new AdminUserDetailsApiResponse();

            try
            {
                var response = await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/account/details", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<AdminUserDetailsApiResponse>(content);
                }
                if (response.ReasonPhrase == "Bad Request")
                {
                    result = new AdminUserDetailsApiResponse
                    {
                        ErrorCode = -1000,
                        ErrorDescription = "General Error",
                    };
                }

            }
            catch (Exception ex)
            {
                result = new AdminUserDetailsApiResponse
                {
                    ErrorCode = -1000,
                    ErrorDescription = "General Error " + ex.Message
                };

            }
            return result;
        }
    }
}
