using Microsoft.AspNetCore.Mvc;
using AdminApi.Models.Account.Login;
using AdminApi.Models.Account.AdminUserCreate;
using AdminApi.Models.Account.AdminUsersList;
using AdminApi.Models.Account.AdminUserUpdate;
using AdminApi.Models.Account.AdminUserDetails;
using AdminApi.Models.Account.SessionCheck;

namespace AdminApi.Controllers
{
    public class AccountController : BaseController
    {
        /** Input Parameter: SessionCheckRequest - String sessionID from FrontEND's JavaScript
         *  (got it from .NET Server - When trying to Connect with Browser - for example). 
         *  Pass SessionID to the AccountManager's SessionCheck.
         *  This is passed to the DAL's SessionCheck.
         *  
         *  Return: JSON Result With SessionCheckResponse- ErrorCode, ErrorDescription.
         *  POST JSON Result in FrontEnd.
         *  */
        [HttpPost]
        [Route("check")]
        public IActionResult SessionCheck([FromBody]SessionCheckRequest request)
        {
            var result = _accountManager.SessionCheck(new DataObjects.Account.SessionCheck.SessionCheckParameters
            {
                SessionId = request.SessionId
            });


            return Json(new SessionCheckResponse
            {
                ErrorCode = result.ErrorCode,
                ErrorDescription = result.ErrorDescription,
            });
        }

        /** Input Parameter: LoginRequest - [Email, Password, IP, DeviceID] from FrontEND's JavaScript
    *  Pass it to AccountManager's Login Method.
    *  This is passed to the DAL's Login Method - Linked with Stored Procedure AdminUserLogIn.
    *  
    *  Return: JSON Result With LoginResponse- ErrorCode, ErrorDescription & SessionID.
    *  POST JSON Result in FrontEnd.
    *  */
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginRequest request)
        {
            var result = _accountManager.Login(new DataObjects.Account.Login.LoginParameters
            {
                Email = request.Email,
                ClientIp = request.ClientIp,
                DeviceNumber = request.DeviceIdentificator,
                Password = request.Password
            });


            return Json(new LoginResponse
            {
                ErrorCode = result.ErrorCode,
                ErrorDescription = result.ErrorDescription,
                SessionId = result.SessionId
            });
        }


        /** Input Parameter: UserDetails - [SesssionID, UserID] from FrontEND
    *  Pass it to AccountManager's AdminUserDetails.
    *  This is passed to the DAL's AdminUserDetails - Linked with Stored Procedure AdminsFetch. 
    *  
    *  Return: JSON Result With AdminUserDetails- ErrorCode, ErrorDescription, Email , FirstName , LastName & Phone.
    *  POST JSON Result in FrontEnd.
    *  */

        [HttpPost]
        [Route("details")]
        public IActionResult UserDetails([FromBody]AdminUserDetailsRequest userDetailsRequest)
        {

            var result = _accountManager.AdminUserDetails(new DataObjects.Account.AdminUserDetails.AdminUserDetailsParameters
            {
                SessionId = userDetailsRequest.SessionId,
                UserId = userDetailsRequest.UserId
            });

            /** Build JSON Object with specified parameters from Business Logic's Response*/
            return Json(new AdminUserDetailsResponse
            {
                ErrorCode = 0,
                ErrorDescription = "Success",
                Email = result.Email,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Phone = result.Phone
            });
        }

        /** Input Parameter: AdminUserDetails to be Updated [AdminUserUpdateRequest] - from FrontEND & All specific
         * AdminUserDetails to find reference.
         *  Pass it to AccountManager's AdminUserUpdate
         *  This is passed to the DAL's AdminUserUpdate - Linked with Stored Procedure AdminUpdate 
         *  
         *  Return: JSON Result Indicating a Successful Update Operation - ErrorCode, ErrorDescription
         *  POST JSON Result in FrontEnd.
         *  */


        [HttpPost]
        [Route("update")]
        public IActionResult AdminUserUpdate([FromBody]AdminUserUpdateRequest userDetailsRequest)
        {

            var result = _accountManager.AdminUserUpdate(new DataObjects.Account.AdminUserUpdate.AdminUserUpdateParameters
            {
                SessionId = userDetailsRequest.SessionId,
                FirstName = userDetailsRequest.FirstName,
                LastName = userDetailsRequest.LastName,
                Permissions = userDetailsRequest.Permissions,
                Phone = userDetailsRequest.Phone,
                UserId = userDetailsRequest.UserId
            });

            return Json(new AdminUserUpdateResponse
            {
                ErrorCode = 0,
                ErrorDescription = "Success"
            });
        }


        /** Input Parameter: AdminUserCreateRequest containing all AdminUser Details - from FrontEND.
       *  Pass it to AccountManager's AdminUserCreate
       *  This is passed to the DAL's AdminUserCreate - Linked with Stored Procedure AdminCreate
       *  
       *  Return: JSON Result Indicating a Successful Admin User Create Operation - ErrorCode, ErrorDescription & Returned Newly created User's ID.
       *  POST JSON Result in FrontEnd.
       *  */


        [HttpPost]
        [Route("create")]
        public IActionResult AdminUserCreate([FromBody]AdminUserCreateRequest request)
        {
            var result = _accountManager.AdminUserCreate(new DataObjects.Account.AdminUserCreate.AdminUserCreateParameters
            {
                SessionId = request.SessionId,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Permissions = request.Permissions,
                Password = request.Password
            });

            return Json(new AdminUserCreateResponse
            {
                ErrorCode = result.ErrorCode,
                ErrorDescription = result.ErrorDescription,
                UserId = result.UserId
            });
        }

        /** Input Parameter: AdminUserListRequest containing SessionID - from FrontEND.
     *  Pass it to AccountManager's AdminUserList
     *  This is passed to the DAL's AdminUserList- Linked with Stored Procedure AdminFetch
     *  
     *  Return: JSON Result Indicating a Successful Admin Users Fetch Operation - SessionID & List<AdminUser> Data.
     *  POST JSON Result in FrontEnd.
     *  */

        [HttpPost]
        [Route("list")]
        public IActionResult AdminUsersList([FromBody]AdminUsersListRequest request)
        {
            var result = _accountManager.AdminUsersList(new DataObjects.Account.AdminUsersList.AdminUsersListParameters
            {
                SessionId = request.SessionId,
            });

            return Json(result);
        }
    }
}
