using System.Threading.Tasks;
using PenaltiesManagement.APIServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using PenaltiesManagement.Models.Entities;
using System.Text;
using System.Net.Http;
using PenaltiesManagement.Models;
using PenaltiesManagement.Models.API.SystemApi.AppealTemplatesList;
using PenaltiesManagement.Models.API.SystemApi.AppealTemplateAdd;
using PenaltiesManagement.Models.API.SystemApi.AppealTemplateFetch;
using PenaltiesManagement.Models.API.SystemApi.AppealTemplateFetchData;
using PenaltiesManagement.Models.API.SystemApi.AppealTemplateDelete;

namespace AdminApi.APIServices
{
    public class LookupApiService : BaseApiClient
    {
        public async Task<AppealTemplateDeleteApiResponse> AppealTemplateDelete(AppealTemplateDeleteApiRequest request)
        {
            var result = new AppealTemplateDeleteApiResponse();
            try
            {
                var response = await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/lookup/templates/appeal/delete", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<AppealTemplateDeleteApiResponse>(content);
                }
            }
            catch (Exception ex)
            {
                //TBD
            }
            return result;
        }


        public async Task<AppealTemplateAddApiResponse> AppealTemplateAdd(AppealTemplateAddApiRequest request)
        {
            var result = new AppealTemplateAddApiResponse();
            try
            {
                var response = await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/lookup/templates/appeal/add", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<AppealTemplateAddApiResponse>(content);
                }
            }
            catch (Exception ex)
            {
                //TBD
            }
            return result;
        }
        public async Task<AppealTemplateFetchDataApiResponse> AppealTemplateFetchData(AppealTemplateFetchDataApiRequest request)
        {
            var result = new AppealTemplateFetchDataApiResponse();
            try
            {
                var response = await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/lookup/templates/appeal/data", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<AppealTemplateFetchDataApiResponse>(content);
                }
            }
            catch (Exception ex)
            {
                //TBD
            }
            return result;
        }

        public async Task<AppealTemplateFetchApiResponse> AppealTemplateFetch(AppealTemplateFetchApiRequest request)
        {
            var result = new AppealTemplateFetchApiResponse();
            try
            {
                var response = await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/lookup/templates/appeal/fetch", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<AppealTemplateFetchApiResponse>(content);
                }
            }
            catch (Exception ex)
            {
                //TBD
            }
            return result;
        }


        public async Task<AppealTemplatesListApiResponse> AppealTemplatesList(AppealTemplatesListApiRequest request)
        {
            var result = new AppealTemplatesListApiResponse();
            try
            {
                var response = await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/lookup/templates/appeal/list", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<AppealTemplatesListApiResponse>(content);
                }
            }
            catch (Exception ex)
            {
                //TBD
            }
            return result;
        }


        public async Task<List<DocumentType>> ClientDocumentTypes()
        {
            var result = new List<DocumentType>();
            try
            {
                // Send a GET Request to the Specified URI. Get a List of DocumentType [Category,DocumentTypeID, CategoryName] 
                // from WebServiceAPI
                var response = await _client.GetAsync(ApiLinks.AdminApiUrl + "api/v1/admin/lookup/document/types/client");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<DocumentType>>(content);
                }
            }
            catch (Exception ex)
            {
                //TBD
            }
            return result;
        }

        /**  Add a new Document Details [without Blob] to FrontEnd - POST */
        public async Task<BaseResponseModel> ClientDocumentTypesAdd(DocumentType request)
        {
            try
            {
                await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/lookup/document/types/client/add", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));

            }
            catch (Exception ex)
            {
                //TBD
            }
            return new BaseResponseModel
            {
                ErrorCode=0,
                ErrorDescription="Success"
            };
        }

        /**  Update a new Document Details [without Blob] to FrontEnd - POST */

        public async Task<BaseResponseModel> ClientDocumentTypesUpdate(DocumentType request)
        {
            try
            {
                await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/lookup/document/types/client/update", new StringContent(JsonConvert.SerializeObject(request).ToString(), Encoding.UTF8, "application/json"));

            }
            catch (Exception ex)
            {
                //TBD
            }
            return new BaseResponseModel
            {
                ErrorCode = 0,
                ErrorDescription = "Success"
            };
        }

        /** Get All Statuses - JSON Object from WebAPI. Deserialized Object -Retrieve Status List data to FrontEnd */
        public async Task<List<StatusEntity>> GetStatuses()
        {
            var result = new List<StatusEntity>();
            try
            {
                var response = await _client.GetAsync(ApiLinks.AdminApiUrl + "api/v1/admin/lookup/statuses");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<StatusEntity>>(content);
                }
            }
            catch (Exception ex)
            {
                //TBD
            }
            return result;
        }

        /** Get All Statuses - JSON Object from WebAPI. Deserialized Object -Retrieve Status List data to FrontEnd */

        public async Task<List<StatusEntity>> GetStatusDetails(int statusId)
        {
            var result = new List<StatusEntity>();
            try
            {
                var response = await _client.GetAsync(ApiLinks.AdminApiUrl + "api/v1/admin/lookup/statuses/" + statusId.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<StatusEntity>>(content);
                }
            }
            catch (Exception ex)
            {
                //TBD
            }
            return result;
        }

        /** Add a specific status to FrontEnd */

        public async Task<BaseResponseModel> AddStatus(StatusEntity status)
        {
            try
            {
                await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/lookup/statuses/add", new StringContent(JsonConvert.SerializeObject(status).ToString(), Encoding.UTF8, "application/json"));

            }
            catch (Exception ex)
            {
                //TBD
            }
            return new BaseResponseModel
            {
                ErrorCode = 0,
                ErrorDescription = "Success"
            };
        }

        /** Update specific status to FrontEnd */
        public async Task<BaseResponseModel> UpdateStatus(StatusEntity status)
        {
            try
            {
                await _client.PostAsync(ApiLinks.AdminApiUrl + "api/v1/admin/lookup/statuses/update", new StringContent(JsonConvert.SerializeObject(status).ToString(), Encoding.UTF8, "application/json"));

            }
            catch (Exception ex)
            {
                //TBD
            }
            return new BaseResponseModel
            {
                ErrorCode = 0,
                ErrorDescription = "Success"
            };
        }
    }
}
