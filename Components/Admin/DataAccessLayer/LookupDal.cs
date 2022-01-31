using DataObjects.Entities;
using DataObjects.Lookup.AppealTemplates;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace DataAccessLayer
{
    #region Public Methods
    public class LookupDal : BaseDal
    {
        /** Fetch a List of all DocumentTypes [Category, DocumentTypeID, DocumentTypeName] by category  */
        public List<DocumentType> GetClientDocumentsTypes(int category)
        {
            // List of DocumentTypes
            var result = new List<DocumentType>();

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.LookupDocumentTypesList, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_category", category); // Input Category
                cmd.Parameters["?p_category"].Direction = ParameterDirection.Input;

                conn.Open();


                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        result.Add(new DocumentType
                        {
                            DocumentTypeId = rdr.AsInt("DocumentTypeId"),
                            DocumentTypeName = rdr.AsString("DocumentType")
                        });
                    }
                }

            }
            return result;
        }

        /** Add the specific documentType to the next index per specific Category (After max index of specific category) */
        public void DocumentsTypeAdd(DocumentType documentType)
        {
            // DocumentTypeName- just the name of the file

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.LookupDocumentTypeAdd, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_category", documentType.Category);
                cmd.Parameters["?p_category"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_documentType", documentType.DocumentTypeName);
                cmd.Parameters["?p_documentType"].Direction = ParameterDirection.Input;

                conn.Open();
                cmd.ExecuteNonQuery();


            }

        }

        /** Update Specific Document Name per specific given DocumentID , Category & DocumentType.  */
        public void DocumentsTypeUpdate(DocumentType documentType)
        {
            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.LookupDocumentTypeUpdate, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_category", documentType.Category);
                cmd.Parameters["?p_category"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_documentTypeId", documentType.DocumentTypeId);
                cmd.Parameters["?p_documentTypeId"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_documentType", documentType.DocumentTypeName);
                cmd.Parameters["?p_documentType"].Direction = ParameterDirection.Input;

                conn.Open();
                cmd.ExecuteNonQuery();
                
            }
        }

        /** Display All Statuses for non-existent I.D / Display Specific Status for existent statusID. */
        public List<StatusEntity> GetStatuses(int statusId)
        {
            var result = new List<StatusEntity>();

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.LookupStatusesFetch, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_statusId", statusId);
                cmd.Parameters["?p_statusId"].Direction = ParameterDirection.Input;

                conn.Open();

                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        result.Add(new StatusEntity
                        {
                            StatusId = rdr.AsInt("StatusId"), // convert to Int "StatusID" value.
                            StatusName = rdr.AsString("StatusName"), // convert to String "StatusName" value.
                            Description = rdr.AsString("Description") // convert to String "Description" value.
                        });
                    }
                }

            }
            return result;
        }


        /** Add a new StatusEntity to "Lookup Statuses" Table. */
        public void StatusAdd(StatusEntity status)
        {

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.LookupStatuseAdd, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_statusId", status.StatusId);
                cmd.Parameters["?p_statusId"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_statusName", status.StatusName);
                cmd.Parameters["?p_statusName"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_description", status.Description);
                cmd.Parameters["?p_description"].Direction = ParameterDirection.Input;

                conn.Open();
                cmd.ExecuteNonQuery();


            }

        }

        /** When Given a statusID , update the statusname & description */

        public void StatusUpdate(StatusEntity status)
        {

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.LookupStatuseUpdate, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_statusId", status.StatusId);
                cmd.Parameters["?p_statusId"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_statusName", status.StatusName);
                cmd.Parameters["?p_statusName"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_description", status.Description);
                cmd.Parameters["?p_description"].Direction = ParameterDirection.Input;

                conn.Open();
                cmd.ExecuteNonQuery();


            }

        }


        /**  Add its data to RawData & the new template details with ForeignKey- RawID to templates table.*/ 
        public void AppealTemplateAdd(AppealTemplatesAddParameters template)
        {

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.LookupAppealTemplateAdd, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_sessionid", template.SessionId);
                cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_templatename", template.Template.TemplateName);
                cmd.Parameters["?p_templatename"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_description", template.Template.ShortDescription);
                cmd.Parameters["?p_description"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_raw", template.Template.RawData);
                cmd.Parameters["?p_raw"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_filename", template.Template.OriginalFileName);
                cmd.Parameters["?p_filename"].Direction = ParameterDirection.Input;

                conn.Open();
                cmd.ExecuteNonQuery();


            }

        }
        public AppealTemplateDeleteResult AppealTemplateDelete(AppealTemplateDeleteParameters template)
        {

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.AppealTemplateDelete, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_sessionid", template.SessionId);
                cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_templateid", template.TemplateId);
                cmd.Parameters["?p_templateid"].Direction = ParameterDirection.Input;
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return new AppealTemplateDeleteResult
            {
                ErrorCode = 0
            };

        }

        /** Update its data to RawData & the new template details with ForeignKey- RawID to templates table.*/

        public void AppealTemplateUpdate(AppealTemplatesUpdateParameters template)
        {

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.LookupAppealTemplateUpdate, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_sessionid", template.SessionId);
                cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_templatename", template.Template.TemplateName);
                cmd.Parameters["?p_templatename"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_description", template.Template.ShortDescription);
                cmd.Parameters["?p_description"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_raw", template.Template.RawData);
                cmd.Parameters["?p_raw"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_templateid", template.Template.TemplateId);
                cmd.Parameters["?p_templateid"].Direction = ParameterDirection.Input;

                conn.Open();
                cmd.ExecuteNonQuery();


            }

        }

        /** Per Specific Session ID - if the user is logged in - Retrieve a List of all AppealTemplates
         *  List<AppealTemplates> - templateid, templatename,created,updated,rawid*/
        public AppealTemplateListResult AppealTemplateList(AppealTemplateListParameters parameters)
        {
            var result = new AppealTemplateListResult
            {
                ErrorCode = 0,
                ErrorDescription = "OK",
                Templates = new List<AppealTemplateEntity>()
            };

            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.LookupAppealTemplateList, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_sessionid", parameters.SessionId);
                cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input;

                conn.Open();

                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var template = new AppealTemplateEntity
                        {
                            Created = rdr.AsDate("createdAt"),
                            Updated = rdr.AsDate("updateAt"),
                            RawId = rdr.AsInt("rawid"),
                            TemplateId = rdr.AsInt("templateid"),
                            TemplateName = rdr.AsString("templatename"),
                            ShortDescription = rdr.AsString("description"),
                            OriginalFileName = rdr.AsString("filename"),
                        };
                        result.Templates.Add(template);
                    };
                }
            }

            return result;
        }

        /** Per Specific Session ID - if the user is logged in - 
         * Fetch all data of specific AppealTemplate for given templateid - Created,Updated,RawID, TemplateID, Filename.
           This data represents the appealtemplate technical details (without blob)*/
        public AppealTemplateFetchResult AppealTemplateFetch(AppealTemplateFetchParameters parameters)
        {
            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.LookupAppealTemplateFetch, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_sessionid", parameters.SessionId);
                cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_templateid", parameters.TemplateId);
                cmd.Parameters["?p_templateid"].Direction = ParameterDirection.Input;

                conn.Open();

                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new AppealTemplateFetchResult
                        {
                            ErrorCode = 0,
                            ErrorDescription = "OK",
                            Created = rdr.AsDate("createdAt"),
                            Updated = rdr.AsDate("updateAt"),
                            RawId = rdr.AsInt("rawid"),
                            TemplateId = rdr.AsInt("templateid"),
                            Filename = rdr.AsString("filename")
                        };
                    }
                    else
                    {
                        return new AppealTemplateFetchResult
                        {
                            ErrorCode = -1,
                            ErrorDescription = "No Data"
                        };
                    };
                }
            }
        }

        /** When given a RawID & SessionID , provide the FileName & the RawData - (With Blob)  */
        public AppealTemplateFetchDataResult AppealTemplateFetchData(AppealTemplateFetchDataParameters parameters)
        {
            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.LookupAppealTemplateFetchData, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_sessionid", parameters.SessionId);
                cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_rawid", parameters.RawId);
                cmd.Parameters["?p_rawid"].Direction = ParameterDirection.Input;

                conn.Open();

                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new AppealTemplateFetchDataResult
                        {
                            ErrorCode = 0,
                            ErrorDescription = "OK",
                            RawData= rdr.AsByteArray("data"),
                            FileName = rdr.AsString("filename")
                        };
                    }
                    else
                    {
                        return new AppealTemplateFetchDataResult
                        {
                            ErrorCode = -1,
                            ErrorDescription = "No Data"
                        };
                    };
                }
            }
        }

    }
    #endregion

    #region Private Methods
    #endregion
}

