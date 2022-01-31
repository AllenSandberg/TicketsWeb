using PenaltiesManagement.Models.AdminUserCreate;
using PenaltiesManagement.Models.Entities;
using PenaltiesManagement.Models.System.AdminUserDetails;
using PenaltiesManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System;
using PenaltiesManagement.Models.System.AppealTemplateAdd;

namespace PenaltiesManagement.Controllers
{
    public class SystemController : BaseController
    {
        /** These methods respond as a result of URL Route */


        /** Retrieve AdminUsersList - Send a Request to Web-API Server [SessionID that we got from .NET Server (from BaseController)].
        *   Recieve from WebAPI the response -List<AdminUsers> with all Users' Details .
        *   Put this newly created AdminUser FetchedList to the AdminUserView for Display
        */
        [HttpGet]
        [Route("/system/users")]
        public async Task<IActionResult> AdminUsersList()
        {
            var result = await _accountApiService.AdminUserList(new Models.API.AccountApi.AdminUsersList.AdminUsersListApiRequest 
            {
                SessionId = SessionId
            });

            var users = new System.Collections.Generic.List<AdminUser>();
            foreach (var user in result.AdminUsers)
            {
                AdminUser userAdmin = new AdminUser
                {
                    CretedByUserID = user.CreatedByUser,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    RegistrationDate = user.RegistrationDate,
                    UserId = user.UserId
                };
                users.Add(userAdmin);
            }

            AdminUsersListViewModel viewModel = new AdminUsersListViewModel
            {
                AdminUsers = users
            };

            return View(viewModel);
        }
        /** Retrieve AdminUser's specific details - Send a Request to Account-API Service [SessionID that we got from .NET Server (from BaseController)
         *  & UserID].
        *   Recieve from WebAPI the response -Specific Admin User with all Users' Details .
        *   Put this newly created AdminUser to the AdminUserView for Display
        */
        [HttpGet]
        [Route("/system/users/{userid}")]
        public async Task<IActionResult> AdminUserDetails(int userid,AdminUserDetailsRequestModel request)
        {
            var result = await _accountApiService.AdminUserDetails(new Models.API.AccountApi.AdminUserDetails.AdminUserDetailsApiRequest 
            {
                SessionId = SessionId,
                UserId= request.UserId,
            });

            return View(new AdminUserDetailsViewModel
            {
                UserId=userid,
                Email= result.Email,
                FirstName= result.FirstName,
                LastName= result.LastName,
                Phone= result.Phone
            });
        }

        /** Send AdminUser's specific details - Send a Request to Account-API Service [SessionID that we got from .NET Server (from BaseController)
        *  & UserID].
       *   Update Specific Admin User with all Users' Details into the Account API - to Business Logic - to DAL- to Database .
       *   Get a returned updated object and return it here
       */
        [HttpPost]
        [Route("/system/users/{userid}")]
        public async Task<JsonResult>AdminUserUpdate(int userid, AdminUserDetailsUpdateRequestModel request)
        {
            var result = await _accountApiService.AdminUserUpdate(new Models.API.AccountApi.AdminUserUpdate.AdminUserUpdateApiRequest
            {
                SessionId = SessionId,
                UserId = request.UserId,
                Phone=request.Phone,
                LastName= request.LastName,
                FirstName= request.FirstName,
                Permissions= request.Permissions
            });

            return Json(result);
        }


        [HttpGet]
        [Route("/system/users/create")]
        public IActionResult AdminUserCreate()
        {
            return View();
        }


        /** Pass AdminUserCreateRequestModel  - Admin User create Details to AccountAPI_Service. 
         * Return the Newly Created Result to Frontend */
        [HttpPost]
        [Route("/system/users/create")]
        public async Task<JsonResult> AdminUserCreate(AdminUserCreateRequestModel request)
        {
            var result = await _accountApiService.AdminUserCreate(new PenaltiesManagement.Models.API.AccountApi.Registration.AdminUserCreateApiRequest
            {
                SessionId=SessionId,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                Permissions = request.Permissions
            });

            return Json(result);
        }

        [HttpGet]
        [Route("/system/document/templates/view/{rawId}")]
        public async Task<FileContentResult> AppealTemplateDownload(int rawId)
        {
            var result = await _lookupApiService.AppealTemplateFetchData(new Models.API.SystemApi.AppealTemplateFetchData.AppealTemplateFetchDataApiRequest
            {
                SessionId = SessionId,
                RawId= rawId
            });
            Response.Headers.Add("content-disposition", "attachment; filename=" + result.FileName);
            byte[] byteArray = Convert.FromBase64String(System.Text.Encoding.Default.GetString(result.RawData));

            return new FileContentResult(byteArray, "application/octet-stream");
        }


        [HttpGet]
        [Route("/system/document/templates/add")]
        public IActionResult AppealTemplateAdd()
        {
            return View();
        }
        [HttpGet]
        [Route("/system/document/templates/delete/{templateId}")]
        public async Task<IActionResult> AppealTemplateDelete(int templateId)
        {
            var response = await _lookupApiService.AppealTemplateDelete(new Models.API.SystemApi.AppealTemplateDelete.AppealTemplateDeleteApiRequest
            {
                SessionId = SessionId,
                TemplateId=templateId
            });
            //return Json(response);
            return RedirectToAction("AppealTemplatesList", "System");
        }

        [HttpPost]
        [Route("/system/document/templates/add")]
        public async Task<JsonResult> AppealTemplateAdd(IFormFile file, AppealTemplateAddRequestModel requestModel)
        {
            byte[] fileData;
            if (file != null && file.Length > 0 && requestModel.TemplateName.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    fileData = Encoding.UTF8.GetBytes(Convert.ToBase64String(stream.ToArray()));
                }
                var response = await _lookupApiService.AppealTemplateAdd(new Models.API.SystemApi.AppealTemplateAdd.AppealTemplateAddApiRequest
                {
                    SessionId = SessionId,
                    Template=new AppealTemplateEntity
                    {
                        TemplateName=requestModel.TemplateName,
                        ShortDescription=requestModel.TemplateDescription,
                        RawData=fileData,
                        OriginalFileName=file.FileName
                    }
                });
                return Json(response);
            }

            return Json(new AppealTemplateAddResponseModel
            {
                ErrorCode = -1000,
                ErrorDescription = "בעיה בקובץ"
            }
            );
        }
        [HttpGet]
        [Route("/system/document/templates")]
        public async Task<IActionResult> AppealTemplatesList()
        {
            var result = await _lookupApiService.AppealTemplatesList(new Models.API.SystemApi.AppealTemplatesList.AppealTemplatesListApiRequest
            {
                SessionId=SessionId
            });

            AppealTemplatesViewModel viewModel = new AppealTemplatesViewModel();
            viewModel.Templates = new System.Collections.Generic.List<AppealTemplateEntity>();

            foreach (var template in result.Templates)
            {
                var newTemplate = new AppealTemplateEntity
                {
                    RawId=template.RawId,
                    Created=template.Created,
                    ShortDescription=template.ShortDescription,
                    TemplateId=template.TemplateId,
                    TemplateName=template.TemplateName,
                    Updated=template.Updated
                };
                viewModel.Templates.Add(newTemplate);
            }
            return View(viewModel); 
        }

        [HttpGet]
        [Route("/system/client/document/types")]
        public async Task<IActionResult> ClientDocumentsTypesList()
        {
            // Send a GET Request to the Specified URI. Get a List of DocumentType [Category,DocumentTypeID, CategoryName] 
            // from WebServiceAPI
            var result = await _lookupApiService.ClientDocumentTypes();

            DocumentsViewModel viewModel = new DocumentsViewModel();
            viewModel.DocumentTypes = new System.Collections.Generic.List<Models.Entities.DocumentType>();
            foreach (var documentType in result)
            {
                DocumentType documentTypeTemp = new DocumentType
                {
                    DocumentTypeName= documentType.DocumentTypeName,
                    DocumentTypeId= documentType.DocumentTypeId
                };
                viewModel.DocumentTypes.Add(documentTypeTemp);
                // Retrieve all document types to the viewmodel
            }
            return View(viewModel); // Show View Model with Document Types.
        }


        /** Add a specific Type of Document.
         *  Add a new Document Details [without Blob] to FrontEnd - POST*/
        [HttpPost]
        [Route("/system/client/document/types/add")]
        public async Task<JsonResult> ClientDocumentsTypesAdd(string documentType)
        {
            var result =await _lookupApiService.ClientDocumentTypesAdd(new DocumentType 
            {
                DocumentTypeName=documentType
            });

            return Json(result);
        }

        /** Creates a view that renders a view to the response. */
        [HttpGet]
        [Route("/system/client/document/types/add")]
        public IActionResult ClientDocumentsTypesAdd()
        {
            return View();
        }

        /**  Update a new Document Details [without Blob] to FrontEnd - POST */

        [HttpPost]
        [Route("/system/client/document/types/update/{documentTypeId}")]
        public async Task<JsonResult> ClientDocumentsTypesUpdate(DocumentType request)
        {
            var result = await _lookupApiService.ClientDocumentTypesUpdate(new DocumentType
            {
                DocumentTypeId= request.DocumentTypeId,
                DocumentTypeName = request.DocumentTypeName
            });

            return Json(result);
        }
        /** Get Updated Document Type from Database to View */
        [HttpGet]
        [Route("/system/client/document/types/update/{documentTypeId}")]
        public async Task<IActionResult> ClientDocumentsTypesUpdate(int documentTypeId)
        {
            var result = await _lookupApiService.ClientDocumentTypes();

            return View(new DocumentType
            {
                DocumentTypeId= documentTypeId,
                DocumentTypeName=result.Find(x=>x.DocumentTypeId==documentTypeId).DocumentTypeName
            });
        }

        /** Retrieve Status List from the Database.
         *  Show a View of Statuses.*/
        [HttpGet]
        [Route("/system/statuses")]
        public async Task<IActionResult> StatusesList()
        {
            var result = await _lookupApiService.GetStatuses();

            StatusesViewModel viewModel = new StatusesViewModel();
            viewModel.Stauses = new System.Collections.Generic.List<StatusEntity>();
            foreach (var status in result)
            {
                StatusEntity newStatus = new StatusEntity
                {
                    Description = status.Description,
                    StatusId = status.StatusId,
                    StatusName=status.StatusName
                };
                viewModel.Stauses.Add(newStatus);
            }
            return View(viewModel);
        }


        [HttpGet]
        [Route("/system/statuses/add")]
        public IActionResult StatusAdd()
        {
            return View();
        }


        /** Add Status - Add Status details to LookupServiceAPI & Return its details to FrontEnd */
        [HttpPost]
        [Route("/system/statuses/add")]
        public async Task<JsonResult> StatusAdd(int statusId,string status,string description)
        {
            var result = await _lookupApiService.AddStatus(new StatusEntity
            {
                StatusId=statusId,
                Description=description,
                StatusName=status
            });

            return Json(result);
        }

        /** Get Status Details per StatusID & Display Them */
        [HttpGet]
        [Route("/system/statuses/{statusid}")]
        public async Task<IActionResult> StatusUpdate(int statusId)
        {
            var result = await _lookupApiService.GetStatusDetails(statusId);

            StatusesViewModel viewModel = new StatusesViewModel();
            viewModel.Stauses = new System.Collections.Generic.List<StatusEntity>
            {
                new StatusEntity
                {
                    Description=result[0].Description,
                    StatusId=result[0].StatusId,
                    StatusName=result[0].StatusName,
                }
            };
            return View(viewModel);
        }

        /** Update Status - Update Status details to LookupServiceAPI & Return its details to FrontEnd */

        [HttpPost]
        [Route("/system/statuses/{statusid}")]
        public async Task<JsonResult> StatusUpdate(int statusId,string status, string description)
        {
            var result = await _lookupApiService.UpdateStatus(new StatusEntity
            {
                Description = description,
                StatusName = status,
                StatusId=statusId
            });

            return Json(result);
        }
           
      
    }
}