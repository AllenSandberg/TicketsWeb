using AdminApi.Models.Entities;
using AdminApi.Models.Lookup.AppealTemapltesList;
using AdminApi.Models.Lookup.AppealTemplateAdd;
using AdminApi.Models.Lookup.AppealTemplateDelete;
using AdminApi.Models.Lookup.AppealTemplateFetch;
using AdminApi.Models.Lookup.AppealTemplateFetchData;
using AdminApi.Models.Lookup.AppealTemplateUpdate;
using Microsoft.AspNetCore.Mvc;

namespace AdminApi.Controllers
{
    public class LookupController : BaseController
    {

        /** Fetch a List of all DocumentTypes (Documents) [Category, DocumentTypeID, DocumentTypeName] by category.
         *  Return : JSON wrapped List. POST in FrontEnd */

        [HttpGet]
        [Route("document/types/client")]
        public IActionResult DocumentTypesClient()
        {
            var result = _lookupManager.GetDocumentTypes(1);

            return Json(result);
        }

        /** Input : Document [Category,ID,Name] details from FrontEnd.
         *  Output : None 
            Operation: Add Document Details (Without Blob) to Database with Lookupmanager's DocumentTypeAdd.
            Using Dal's operation (stored procedure LookupDocumentTypeAdd).
            By Default --> Add with category 1. */
        [HttpPost]
        [Route("document/types/client/add")]
        public IActionResult AddDocumentTypesClient([FromBody]DocumentType documentType)
        {
            _lookupManager.DocumentTypeAdd(new DataObjects.Entities.DocumentType
            {
                Category = 1,
                DocumentTypeId = documentType.DocumentTypeId,
                DocumentTypeName = documentType.DocumentTypeName
            });

            return Json(null);
        }

        /** Input : Document [Category,ID,Name] details from FrontEnd.
        *   Output : None 
           Operation: Update Document Details (Without Blob) to Database with Lookupmanager's DocumentTypeUpdate.
           Using Dal's operation (stored procedure LookupDocumentTypeUpdate).
           By Default --> Update with category 1. */
        [HttpPost]
        [Route("document/types/client/update")]
        public IActionResult UpdateDocumentTypesClient([FromBody]DocumentType documentType)
        {
            _lookupManager.DocumentTypeUpdate(new DataObjects.Entities.DocumentType
            {
                Category = 1,
                DocumentTypeId = documentType.DocumentTypeId,
                DocumentTypeName = documentType.DocumentTypeName
            });

            return Json(null);
        }

        /** Get All Statuses from Database.
         *  Return List<Status> */
        [HttpGet]
        [Route("statuses")]
        public IActionResult GetStatuses()
        {
            var result = _lookupManager.GetStatuses(0);

            return Json(result);
        }

        /** Get Specific Status By Status ID. Return Json(result)
         * */
        [HttpGet]
        [Route("statuses/{statusId}")]
        public IActionResult GetStatuseDetails(int statusId)
        {
            var result = _lookupManager.GetStatuses(statusId);

            return Json(result);
        }

        /** Input: New StatusEntity from FrontEnd.
         *  Add this new status to Database [ID, Name, Description]*/
        [HttpPost]
        [Route("statuses/add")]
        public IActionResult AddStatus([FromBody]StatusEntity status)
        {
            _lookupManager.StatusAdd(new DataObjects.Entities.StatusEntity
            {
                Description = status.Description,
                StatusId = status.StatusId,
                StatusName = status.StatusName
            });

            return Json(null);
        }

        /** Input: Updated StatusEntity Details from FrontEnd.
         *  Update this status details to Database's oldstatus [ID, Name, Description]*/

        [HttpPost]
        [Route("statuses/update")]
        public IActionResult UpdateStatus([FromBody]StatusEntity status)
        {
            _lookupManager.StatusUpdate(new DataObjects.Entities.StatusEntity
            {
                Description = status.Description,
                StatusId = status.StatusId,
                StatusName = status.StatusName
            });

            return Json(null);
        }




        [HttpGet]
        [Route("document/types/merchant")]
        public IActionResult DocumentTypesMerchant()
        {
            var result = _lookupManager.GetDocumentTypes(2);

            return Json(result);
        }

        /** Add Document Type - Category2. */

        [HttpPost]
        [Route("document/types/merchant/add")]
        public IActionResult AddDocumentTypesMerchant([FromBody]DocumentType documentType)
        {
            _lookupManager.DocumentTypeAdd(new DataObjects.Entities.DocumentType
            {
                Category = 2,
                DocumentTypeId = documentType.DocumentTypeId,
                DocumentTypeName = documentType.DocumentTypeName
            });

            return Json(null);
        }

        /* Update Document Type 2 Details*/
        [HttpPost]
        [Route("document/types/merchant/update")]
        public IActionResult UpdateDocumentTypesMerchant([FromBody]DocumentType documentType)
        {
            _lookupManager.DocumentTypeUpdate(new DataObjects.Entities.DocumentType
            {
                Category = 2,
                DocumentTypeId = documentType.DocumentTypeId,
                DocumentTypeName = documentType.DocumentTypeName
            });

            return Json(null);
        }



        [Route("document/types/transaction")]
        public IActionResult DocumentTypesTransaction()
        {
            var result = _lookupManager.GetDocumentTypes(3);

            return Json(result);
        }


        [HttpPost]
        [Route("document/types/transaction/add")]
        public IActionResult AddDocumentTypesTransaction([FromBody]DocumentType documentType)
        {
            _lookupManager.DocumentTypeAdd(new DataObjects.Entities.DocumentType
            {
                Category = 3,
                DocumentTypeId = documentType.DocumentTypeId,
                DocumentTypeName = documentType.DocumentTypeName
            });

            return Json(null);
        }


        [HttpPost]
        [Route("document/types/transaction/update")]
        public IActionResult UpdateDocumentTypesTransaction([FromBody]DocumentType documentType)
        {
            _lookupManager.DocumentTypeUpdate(new DataObjects.Entities.DocumentType
            {
                Category = 3,
                DocumentTypeId = documentType.DocumentTypeId,
                DocumentTypeName = documentType.DocumentTypeName
            });

            return Json(null);
        }

        /** Input : From Frontend - SessionID.
         *  Return: to FrontEnd - in Json Format - A List of all AppealTemplates
         - templateid, templatename,created,updated,rawid.
            Per Specific User's SessionID */

        [HttpPost]
        [Route("templates/appeal/list")]
        public IActionResult AppealTemapltesList([FromBody]AppealTemaplatesListRequest request)
        {
            var result = _lookupManager.AppealTemplateList(new DataObjects.Lookup.AppealTemplates.AppealTemplateListParameters
            {
                SessionId = request.SessionId
            });

            return Json(result);
        }


        /** Add a new AppealTemplate with its BLOB Content - per Given User SessionID..
         *  AppealTemplateAddRequest - details from FrontEnd.
         *  Return: to FrontEnd an AppealTemplateAddResponse Successful Add Operation.
         * */
        [HttpPost]
        [Route("templates/appeal/add")]
        public IActionResult AppealTemaplteAdd([FromBody]AppealTemplateAddRequest request)
        {
            _lookupManager.AppealTemplateAdd(new DataObjects.Lookup.AppealTemplates.AppealTemplatesAddParameters
            {
                SessionId = request.SessionId,
                Template=new DataObjects.Entities.AppealTemplateEntity
                {
                    ShortDescription=request.Template.ShortDescription,
                    TemplateName=request.Template.TemplateName,
                    RawData=request.Template.RawData,
                    OriginalFileName=request.Template.OriginalFileName
                }

            });

            return Json(new AppealTemplateAddResponse
            {
                ErrorCode = 0,
                ErrorDescription = "OK"
            });
        }
        [HttpPost]
        [Route("templates/appeal/delete")]
        public IActionResult AppealTemaplteDelete([FromBody]AppealTemplateDeleteRequest request)
        {
            _lookupManager.AppealTemplateDelete(new DataObjects.Lookup.AppealTemplates.AppealTemplateDeleteParameters
            {
                SessionId = request.SessionId,
                TemplateId=request.TemplateId
            });

            return Json(new AppealTemplateDeleteResponse
            {
                ErrorCode = 0,
                ErrorDescription = "OK"
            });
        }

        /** Update an AppealTemplate's Details - per Given User SessionID.
       *  AppealTemplateUpdateRequest - AppealTemplate Update details from FrontEnd.
       *  Return: to FrontEnd an AppealTemplateUpdateResponse Successful Add Operation.
       * */
        [HttpPost]
        [Route("templates/appeal/update")]
        public IActionResult AppealTemaplteUpdate([FromBody]AppealTemplateUpdateRequest request)
        {
            _lookupManager.AppealTemplateUpdate(new DataObjects.Lookup.AppealTemplates.AppealTemplatesUpdateParameters
            {
                SessionId = request.SessionId,
                Template = new DataObjects.Entities.AppealTemplateEntity
                {
                    Created = request.Template.Created,
                    RawData = request.Template.RawData,
                    RawId = request.Template.RawId,
                    ShortDescription = request.Template.ShortDescription,
                    TemplateId = request.Template.TemplateId,
                    TemplateName = request.Template.TemplateName,
                    Updated = request.Template.Updated
                }
            });

            return Json(new AppealTemplateUpdateResponse
            {
                ErrorCode = 0,
                ErrorDescription = "OK"
            });
        }

        /** Update an AppealTemplate's Details - per Given User SessionID.
    *  AppealTemplateUpdateRequest - AppealTemplate Update details from FrontEnd.
    *  Return: to FrontEnd an AppealTemplateUpdateResponse Successful Add Operation.
    * */

        [HttpPost]
        [Route("templates/appeal/fetch")]
        public IActionResult AppealTemplateFetch([FromBody]AppealTemplateFetchRequest request)
        {
            var result = _lookupManager.AppealTemplateFetch(new DataObjects.Lookup.AppealTemplates.AppealTemplateFetchParameters
            {
                SessionId = request.SessionId,
                TemplateId = request.TemplateId
            });

            return Json(result);
        }
        /** When given a RawID & SessionID - Request from FrontEnd, provide the FileName & the RawData - (With Blob) of AppealTemplate .
         *  Return this Result to the FrontEnd*/


        [HttpPost]
        [Route("templates/appeal/data")]
        public IActionResult AppealTemplateFetchData([FromBody]AppealTemplateFetchDataRequest request)
        {
            var result = _lookupManager.AppealTemplateFetchData(new DataObjects.Lookup.AppealTemplates.AppealTemplateFetchDataParameters
            {
                SessionId = request.SessionId,
                RawId = request.RawId
            });

            return Json(result);
        }
    }
}
