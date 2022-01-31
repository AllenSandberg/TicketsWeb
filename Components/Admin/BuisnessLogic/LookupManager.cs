using DataAccessLayer;
using DataObjects.Entities;
using DataObjects.Lookup.AppealTemplates;
using System;
using System.Collections.Generic;

namespace BuisnessLogic
{
    public class LookupManager
    {
        #region Fields

        private readonly LookupDal _lookupDal;

        #endregion
        #region Ctor

        public LookupManager()
        {
            _lookupDal = new LookupDal();
        }

        #endregion

        #region Public Methods
        /** Fetch a List of all DocumentTypes [Category, DocumentTypeID, DocumentTypeName] by category  */

        public List<DocumentType> GetDocumentTypes(int category)
        {
            var result = _lookupDal.GetClientDocumentsTypes(category);
            return result;
        }

        /** Add the specific documentType to the next index per specific Category (After max index of specific category) */

        public void DocumentTypeAdd(DocumentType documentType)
        {
            _lookupDal.DocumentsTypeAdd(documentType);
        }

        /** Update Specific Document Name per specific given DocumentID , Category & DocumentType.  */

        public void DocumentTypeUpdate(DocumentType documentType)
        {
            _lookupDal.DocumentsTypeUpdate(documentType);
        }

        /** Display All Statuses for non-existent I.D / Display Specific Status for existent statusID. 
            Return List of Statuses for Display*/

        public List<StatusEntity> GetStatuses(int statusId)
        {
            var result = _lookupDal.GetStatuses(statusId);
            return result;
        }

        /** Add a new StatusEntity to "Lookup Statuses" Table. */

        public void StatusAdd(StatusEntity status)
        {
            _lookupDal.StatusAdd(status);
        }

        /** When Given a statusID , update the statusname & description */
        public void StatusUpdate(StatusEntity status)
        {
            _lookupDal.StatusUpdate(status);
        }

        /** Per Specific Session ID - if the user is logged in - Retrieve a List of all AppealTemplates
         List<AppealTemplates> - templateid, templatename,created,updated,rawid*/
        public AppealTemplateListResult AppealTemplateList(AppealTemplateListParameters parameters)
        {
            var result = _lookupDal.AppealTemplateList(parameters);
            return result;
        }

        /** Per Specific Session ID - if the user is logged in - 
         * Fetch all data of specific AppealTemplate for given templateid - Created,Updated,RawID, TemplateID, Filename.
           This data represents the appealtemplate technical details (without blob)*/
        public AppealTemplateFetchResult AppealTemplateFetch(AppealTemplateFetchParameters parameters)
        {
            var result = _lookupDal.AppealTemplateFetch(parameters);
            return result;
        }

        /** When given a RawID & SessionID , provide the FileName & the RawData - (With Blob) of AppealTemplate  */

        public AppealTemplateFetchDataResult AppealTemplateFetchData(AppealTemplateFetchDataParameters parameters)
        {
            var result = _lookupDal.AppealTemplateFetchData(parameters);
            return result;
        }

        /** Add the new template details with ForeignKey- RawID to templates table & Add its data to RawData Table.*/
        public void AppealTemplateAdd(AppealTemplatesAddParameters parameters)
        {
            _lookupDal.AppealTemplateAdd(parameters);
        }

        public AppealTemplateDeleteResult AppealTemplateDelete(AppealTemplateDeleteParameters parameters)
        {
            return _lookupDal.AppealTemplateDelete(parameters);
        }

        /** Update its data to RawData & the new template details with ForeignKey- RawID to templates table.*/

        public void AppealTemplateUpdate(AppealTemplatesUpdateParameters parameters)
        {
            _lookupDal.AppealTemplateUpdate(parameters);
        }

        #endregion

    }
}
