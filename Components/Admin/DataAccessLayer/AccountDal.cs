using DataObjects.Account.AdminUserCreate;
using DataObjects.Account.AdminUserDetails;
using DataObjects.Account.AdminUsersList;
using DataObjects.Account.AdminUserUpdate;
using DataObjects.Account.Login;
using DataObjects.Account.SessionCheck;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataAccessLayer
{
    #region Public Methods
    /** This class extends the BaseDal, that contains the basic GetConnection() method.
     *  This means - from any method here by calling GetConnection() we can access the existing
     *  MySqlConnection to the existing database with the right connection string already.
     *  
     *  This class is responsible for Account Operations in the Database.
     * */
    public class AccountDal : BaseDal
    {

        /** Get a SessionCheckParameters Data Object (containing SessionID). */
        public SessionCheckResult SessionCheck(SessionCheckParameters parameters)
        {
            using (MySqlConnection conn = GetConnection())
            {
                // MySqlCommand gets as parameter - the command_name, connection object. 
                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.SessionCheck, conn);
                cmd.CommandType = CommandType.StoredProcedure; // set the MySQLCommandType to StoredProcedure
                cmd.Parameters.AddWithValue("?p_sessionid", parameters.SessionId);
                // SessionCheckParamaters (Session I.D) is input-only
                cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input;
                conn.Open();
                
                //MySqlDataReader - Reads a stream of rows from DB. 

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    // Advance the DataReader to the next record.
                    if (reader.Read())
                    {

                        // If tocontinue column value == 1 , Success - Session isn't expired yet.
                        if (reader.AsInt("tocontinue") ==1)
                        {
                            return new SessionCheckResult
                            {
                                ErrorCode = 0,
                                ErrorDescription = "Success",
                            };

                        }
                        else
                        {
                            // Failure - Session is expired.
                            return new SessionCheckResult
                            {
                                ErrorCode = -1,
                                ErrorDescription = "No session",
                            };
                        }
                    }
                }
                return new SessionCheckResult
                {
                    ErrorCode = -1,
                    ErrorDescription = "No session"
                };

            }
        }

        /** This method is linked to Stored Procedure - AdminsFetch. 
         * Retrieve specific Admin Details from DB By AdminUserParameters [ String sessionID, int UserID] */
        public AdminUserDetailsResult AdminUserDetails(AdminUserDetailsParameters parameters)
        {
            
            // AdminUserDetailsResult Object [FirstName,LastName,Email,Phone]
            AdminUserDetailsResult result = new AdminUserDetailsResult();

            using (MySqlConnection conn = GetConnection())
            {
                // MySqlCommand gets as parameter - the command_name, connection object. 
                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.AdminsFetch, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_sessionid", parameters.SessionId); // 1st procedure parameter - Add SessionId value
                cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input; // Input Parameter
                cmd.Parameters.AddWithValue("?p_userid", parameters.UserId); // 2nd Procedure Parameter - Add UserId value
                cmd.Parameters["?p_userid"].Direction = ParameterDirection.Input; // Input Parameter
                conn.Open();

               //MySqlDataReader - Reads a stream of rows from DB's procedure result value.
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    result.ErrorCode = 0;
                    result.ErrorDescription = "Success";


                    if (rdr.Read())
                    {
                        result.Email = rdr.AsString("email"); // set AdminUserDetailsResult's Email to be the read Email Result
                        result.FirstName = rdr.AsString("fname"); // set AdminUserDetailsResult's Email to be the read FirstName Result
                        result.LastName = rdr.AsString("lname"); // set AdminUserDetailsResult's Email to be the read LastName Result
                    }
                }
            }

            return result;
        }

        /** This method is linked to Stored Procedure - AdminUserLogin 
        * Retrieve specific Admin Details from DB By AdminUserParameters [ String sessionID, int UserID] */
        public LoginResult Login(LoginParameters parameters)
        {
            string sessionid = ""; // Returned SessionID As a Result of Login

            using (MySqlConnection conn = GetConnection())
            {

                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.AdminUserLogin, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_email", parameters.Email); // Specifying Admin's Email to Procedure - Input parameter
                cmd.Parameters["?p_email"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_password", parameters.Password); // Specifying Admin's Password to Procedure - Input parameter
                cmd.Parameters["?p_password"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_sessionid", sessionid); // Specifying Admin's SessionID to Procedure - Input parameter
                cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input;
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        /** If GUID Value of sessionID Column is not null - means the procedure create a new SessionID for this user
                         or returned an existing one. */
                        if (reader.AsGuid("sessionid") != null)
                        {
                            return new LoginResult
                            {
                                ErrorCode = 0,
                                ErrorDescription = "Success",
                                SessionId = reader.AsGuid("SessionId").ToString()
                                //SessionId = sessionid
                            };

                        }
                    }
                }
                /* SessionID is null if there is no such email & password match provided in Sign-in. */
                return new LoginResult
                {
                    ErrorCode = 10,
                    ErrorDescription = "Wrong Username or Password"
                };

            }
        }
        // Create a New Admin User & Return the Admin User's ID
        public AdminUserCreateResult AdminUserCreate(AdminUserCreateParameters parameters)
        {

            using (MySqlConnection conn = GetConnection())
            {

                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.AdminCreate, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_sessionid", parameters.SessionId);
                cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_email", parameters.Email);
                cmd.Parameters["?p_email"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_password", parameters.Password);
                cmd.Parameters["?p_password"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_fname", parameters.FirstName);
                cmd.Parameters["?p_fname"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_lname", parameters.LastName);
                cmd.Parameters["?p_lname"].Direction = ParameterDirection.Input;
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        if (reader.AsInt("createduserid") != null)
                        {
                            return new AdminUserCreateResult
                            {
                                ErrorCode = 0,
                                ErrorDescription = "Success",
                                UserId = reader.AsInt("createduserid")
                            };

                        }
                    }
                }
                // If CreateUserID is null / non-existent.
                return new AdminUserCreateResult
                {
                    ErrorCode = 11,
                    ErrorDescription = "Email already exist"
                };

            }

        }

        /** Find UserID By SessionID In Sessions table - and return from Admin Table, Admin Details by UserID */
        public AdminUsersListResult AdminUserList(AdminUsersListParameters parameters)
        {
            AdminUsersListResult result = new AdminUsersListResult();

            using (MySqlConnection conn = GetConnection())
            {

                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.AdminsFetch, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_sessionid", parameters.SessionId);
                cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_userid", 0);
                cmd.Parameters["?p_userid"].Direction = ParameterDirection.Input;
                conn.Open();

                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    result.AdminUsers = new System.Collections.Generic.List<AdminUser>();
                    result.ErrorCode = 0;
                    result.ErrorDescription = "Success";

                    while (rdr.Read())
                    {
                        AdminUser adminUser = new AdminUser
                        {
                            Email = rdr.AsString("email"),
                            UserId = rdr.AsInt("ID"),
                            FirstName= rdr.AsString("fname"),
                            LastName= rdr.AsString("lname"),
                        };
                        result.AdminUsers.Add(adminUser);
                    }
                }
            }

            return result;
        }

        /** AdminUserUpdate - Update First_name/Last_name/Password. */
        public AdminUserUpdateResult AdminUserUpdate(AdminUserUpdateParameters parameters)
        {

            using (MySqlConnection conn = GetConnection())
            {

                MySqlCommand cmd = new MySqlCommand(StoredProcedureNames.AdminUpdate, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("?p_sessionid", parameters.SessionId);
                cmd.Parameters["?p_sessionid"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_password", parameters.Password);
                cmd.Parameters["?p_password"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_userid", parameters.UserId);
                cmd.Parameters["?p_userid"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_fname", parameters.FirstName);
                cmd.Parameters["?p_fname"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?p_lname", parameters.LastName);
                cmd.Parameters["?p_lname"].Direction = ParameterDirection.Input;
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                            return new AdminUserUpdateResult
                            {
                                ErrorCode = 0,
                                ErrorDescription = "Success",
                            };

                }
            }

        }

    }
    #endregion

    #region Private Methods
    #endregion
}
