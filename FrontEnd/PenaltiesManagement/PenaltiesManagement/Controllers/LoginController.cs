using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PenaltiesManagement.Models.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PenaltiesManagement.Controllers
{
    public class LoginController : BaseController
    {
        /** Clean - Cookies,Session in new page. */
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }


        protected override bool SessionRequired { get { return false; } }
        [HttpPost]
        public async Task<JsonResult> Index(LoginRequestModel loginRequest)
        {
            var loginResult = await _accountApiService.Login(new PenaltiesManagement.Models.API.AccountApi.Login.LoginApiRequest
            {
                Email = loginRequest.Email,
                Password = loginRequest.Password,
                DeviceNumber="N/a",
                ClientIp= HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString()

        });
            if (loginResult.ErrorCode == 0 && !string.IsNullOrEmpty(loginResult.SessionId))
            {
                // In the Sesssion Dictionnary Object - put SessionId & Email (that I got from Database).
                // Now we can access them each time from SessionObject 
                HttpContext.Session.SetString("AdminSessionId", $"{loginResult.SessionId}");
                HttpContext.Session.SetString("AdminUserName", $"{loginRequest.Email}");
                return Json(loginResult);
            }
            else if (loginResult.ErrorCode == 10)
            {
                return Json(new LoginResponseModel
                {
                    ErrorCode = 10,
                    ErrorDescription = "Invalid Username or Psssword"
                });
            }
            else
            {
                return Json(new LoginResponseModel
                {
                    ErrorCode = -1000,
                    ErrorDescription = "General Error"
                });
            }

        }
    }
}

