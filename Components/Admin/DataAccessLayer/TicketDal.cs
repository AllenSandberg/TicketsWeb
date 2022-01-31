using DataObjects.Entities;
using DataObjects.Tickets.TicketDocumentAdd;
using DataObjects.Tickets.TicketDocumentFetch;
using DataObjects.Tickets.TicketDocumentsList;
using DataObjects.Tickets.TicketsList;
using DataObjects.Tickets.TicketUpdate;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DataAccessLayer
{
    public class TicketDal : BaseDal
    {
        /** This method is responsible for retrieving Ticket List from the Database. 
         *  This is for the Clerk GUI Interface - of report requests filtered by category.
         *  Return a List<Tickets> with relevant Ticket Data to be displayed.
         *  Return all specific Tickets Object per - sessionID/passport/licensenumber/reportid/phone.
            If there is no specific filter parameter - return all records.*/

        public TicketsListResult TicketsList(DataObjects.Tickets.TicketsList.TicketDocumentsListParameters parameters)
        {
            {
                TicketsListResult result = new TicketsListResult();


                using (MySqlConnection conn = GetConnection())
                {

                    MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.TicketsList, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("?p_sessionid", parameters.SessionId);
                    cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?p_passport", parameters.Passport);
                    cmd.Parameters["?p_passport"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?p_vechile_license", parameters.VichilePlateNumber);
                    cmd.Parameters["?p_vechile_license"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?p_reportid", parameters.PenaltyReportNumber);
                    cmd.Parameters["?p_reportid"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?p_phone", parameters.Phone);
                    cmd.Parameters["?p_phone"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?p_id", parameters.TicketId);
                    cmd.Parameters["?p_id"].Direction = ParameterDirection.Input;
                    conn.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // result holds a List of Ticket Objects- ErrorCode = 0 & ErrorDescription = "Success"
                        result.Tickets = new System.Collections.Generic.List<Ticket>();
                        result.ErrorCode = 0;
                        result.ErrorDescription = "Success";

                        while (reader.Read())
                        {
                            Ticket ticket = new Ticket
                            {
                                Created = reader.AsDate("createdAt"),
                                Updated = reader.AsDate("updatedAt"),
                                Phone = reader.AsString("PhoneNumber"),
                                DriverLicense = reader.AsString("LicenseNumber"),
                                Passport = reader.AsString("IdPassportNumber"),
                                PenaltyReportNumber = reader.AsString("PenaltyNumber"),
                                Status = reader.AsString("OfficeStatus"),
                                SystemTicketId = reader.AsInt("id"),
                                VehiclePlateNumber = reader.AsString("VehiclePlateNumber"),
                                VehileType = reader.AsString("VehicleType"),
                                Comments = reader.AsString("apealReasonFreeText"),
                                LastComment = reader.AsString("lastcomment"),
                                DriverName=reader.AsString("DriverName"),
                                
                            };
                            result.Tickets.Add(ticket);
                        }

                    }
                }
                return result;
            }
        }

        /** This method is responsible for the TicketsUpdate - Status and/or LastComment updated by Admin.
         *  Returns an indicator of Success.
         * */

        public TicketUpdateResult TicketsUpdate(TicketUpdateParameters parameters)
        {
            {
                using (MySqlConnection conn = GetConnection())
                {

                    MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.TicketUpdate, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("?p_sessionid", parameters.SessionId);
                    cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?p_status", parameters.StatusName);
                    cmd.Parameters["?p_status"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?p_ticketid", parameters.TicketId);
                    cmd.Parameters["?p_ticketid"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?p_lastcomment", parameters.LastComment);
                    cmd.Parameters["?p_lastcomment"].Direction = ParameterDirection.Input;
                    conn.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        /** TicketUpdateResult is just an indicator of TicketUpdateSuccess of StoredProcedure TicketUpdate.*/
                        return new TicketUpdateResult
                        {
                            ErrorCode = 0,
                            ErrorDescription = "Success"
                        };
                    }
                }
            }
        }

        public TicketDocumentAddResult TicketDocumentAdd(TicketDocumentAddParameters parameters)
        {
            {
                using (MySqlConnection conn = GetConnection())
                {

                    MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.TicketDocumentAdd, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("?p_sessionid", parameters.SessionId);
                    cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?p_ticketid", parameters.TicketId);
                    cmd.Parameters["?p_ticketid"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?p_imagetype", parameters.DocumentType);
                    cmd.Parameters["?p_imagetype"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?p_filename", parameters.FileName);
                    cmd.Parameters["?p_filename"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?p_data",parameters.DocumentData);
                    cmd.Parameters["?p_data"].Direction = ParameterDirection.Input;
                    conn.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        /** TicketUpdateResult is just an indicator of TicketUpdateSuccess of StoredProcedure TicketUpdate.*/
                        return new TicketDocumentAddResult
                        {
                            ErrorCode = 0,
                            ErrorDescription = "Success"
                        };
                    }
                }
            }
        }

        /** Return Document Type String & BLOB Data - for ticket uploading . */
        public TicketDocumentFetchResult TicketDocumentFetch(TicketDocumentFetchParameters parameters)
        {
            {
                using (MySqlConnection conn = GetConnection())
                {

                    MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.TicketDocumenstFetch, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("?p_sessionid", parameters.SessionId);
                    cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?p_ticketid", parameters.TicketId);
                    cmd.Parameters["?p_ticketid"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?p_documentid", parameters.DocumentId);
                    cmd.Parameters["?p_documentid"].Direction = ParameterDirection.Input;
                    conn.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TicketDocumentFetchResult
                            {
                                DocumentDataBase64 = System.Text.Encoding.Default.GetString(reader.AsByteArray("imagebase64")),
                                DocumentType = reader.AsString("imagetype"),
                                ErrorCode=0,
                                FileName = reader.AsString("originalFileName"),
                            };
                        }
                        else
                        {
                            return new TicketDocumentFetchResult
                            {
                                ErrorCode = 0,
                                ErrorDescription = "No Data"
                            };
                        }

                    }
                }
            }
        }
        /** This method returns a list of a single document object - (id,imagetype,updatedAt,createdAt) for viewing purposes */
        public TicketDocumentsListResult TicketDocumentsList(DataObjects.Tickets.TicketDocumentsList.TicketDocumentsListParameters parameters)
        {
            {
                TicketDocumentsListResult result = new TicketDocumentsListResult();


                using (MySqlConnection conn = GetConnection())
                {

                    MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.TicketDocumenstList, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("?p_sessionid", parameters.SessionId);
                    cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?p_ticketid", parameters.TicketId);
                    cmd.Parameters["?p_ticketid"].Direction = ParameterDirection.Input;
                    conn.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        result.TicketDocuments = new System.Collections.Generic.List<TicketDocument>();
                        result.ErrorCode = 0;
                        result.ErrorDescription = "Success";

                        while (reader.Read())
                        {
                            TicketDocument ticketDocument = new TicketDocument
                            {
                                DocumentId = reader.AsInt("id"),
                                DocumentType = reader.AsString("imageType"),
                                Updated = reader.AsDate("updateddAt"),
                                Created = reader.AsDate("createdAt"),
                                FileName= reader.AsString("originalFileName"),
                            };
                            result.TicketDocuments.Add(ticketDocument);
                        }

                    }
                }
                return result;
            }
        }
    }
}
