using BuisnessLogic.Utils;
using DataAccessLayer;
using DataObjects.Account.AdminUserCreate;
using DataObjects.Account.AdminUserDetails;
using DataObjects.Account.AdminUsersList;
using DataObjects.Account.AdminUserUpdate;
using DataObjects.Account.Login;
using DataObjects.Account.SessionCheck;
using System;

namespace BuisnessLogic
{
    public class AccountManager
    {
        #region Fields

        /** Hold AccountDAL Object for fetching results from AccountDAL's Operations */
        private readonly AccountDal _accountDal;

        #endregion
        #region Ctor

        public AccountManager()
        {
            _accountDal = new AccountDal();
        }

        #endregion

        #region Public Methods
        /** This method is linked to Stored Procedure - AdminsFetch. 
       * Retrieve specific Admin Details from DB By AdminUserParameters [ String sessionID, int UserID].
       * Retrieved Details - FirstName,LastName,Email,Phone - These are returned to the WebAPI.*/
        public AdminUserDetailsResult AdminUserDetails(AdminUserDetailsParameters parameters)
        {
            try
            {
                var result = _accountDal.AdminUserDetails(parameters);
                return result;
            }
            catch (Exception ex)
            {
                return new AdminUserDetailsResult
                {
                    ErrorCode = -1000,
                    ErrorDescription = "General Error"
                };
            }
        }

        /** This method is linked to Stored Procedure - Session Check
     *      Returned Result - Session is Valid [TRUE/FALSE].*/
        public SessionCheckResult SessionCheck(SessionCheckParameters parameters)
        {
            try
            {
                var result = _accountDal.SessionCheck(parameters);
                return result;
            }
            catch (Exception ex)
            {
                return new SessionCheckResult
                {
                    ErrorCode = -1,
                    ErrorDescription = "No session"
                };
            }
        }

        /** This method is Linked to StoredProcedure AdminLogin. HashPassword , Login Admin & Return SessionID per login session. */
        public LoginResult Login(LoginParameters parameters)
        {
            try
            {
                // Hash Password
                parameters.Password = CryptoUtils.HashDataSHA512(parameters.Password);
                var result = _accountDal.Login(parameters);
                return result;
            }
            catch (Exception ex)
            {
                return new LoginResult
                {
                    ErrorCode = -1000,
                    ErrorDescription = "General Error"
                };
            }
        }

        /** This method is Linked to StoredProcedure AdminUserUpdate. 
         *  Returns Successful AdminUserUpdate [TRUE/FALSE]*/

        public AdminUserUpdateResult AdminUserUpdate(AdminUserUpdateParameters parameters)
        {
            try
            {
                var result = _accountDal.AdminUserUpdate(parameters);
                return result;
            }
            catch (Exception ex)
            {
                return new AdminUserUpdateResult
                {
                    ErrorCode = -1000,
                    ErrorDescription = ex.Message
                };
            }
        }

        /** This method is Linked to StoredProcedure AdminUserCreate. 
          *  Returns CreatedUserID TRUE , NULL FALSE */

        public AdminUserCreateResult AdminUserCreate(AdminUserCreateParameters parameters)
        {
            try
            {
                parameters.KeyPassword = CryptoUtils.KeyPasswordProtected;
                parameters.Password = CryptoUtils.HashDataSHA512(parameters.Password);
                /**Hash Password For security reasons, you may want to store passwords in hashed form. 
                  This guards against the possibility that someone who gains unauthorized access to the
                  database can retrieve the passwords of every user in the system. "ONE-WAY" Hash
                 */
                var result = _accountDal.AdminUserCreate(parameters);
                return result;
            }
            catch (Exception ex)
            {
                return new AdminUserCreateResult
                {
                    ErrorCode = -1000,
                    ErrorDescription = ex.Message
                };
            }
        }

        /** Based on Stored Procedure - Admin Fetch 
         * Find UserID By SessionID In Sessions table - and return from Admin Table, 
         * Admin Details by UserID */


        public AdminUsersListResult AdminUsersList(AdminUsersListParameters parameters)
        {
            try
            {
                var result = _accountDal.AdminUserList(parameters);
                return result;
            }
            catch (Exception ex)
            {
                return new AdminUsersListResult
                {
                    ErrorCode = -1000,
                    ErrorDescription = ex.Message
                };
            }
        }


        #endregion

    }
}
