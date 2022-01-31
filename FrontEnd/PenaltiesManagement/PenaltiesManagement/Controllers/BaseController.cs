using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Text;
using AdminApi.APIServices;

namespace PenaltiesManagement.Controllers
{
    /** BaseController Holds References to AccountAPIService,LookupAPIService & TicketsAPIService.
     *  This connects the Controller to the WEBAPI Services.*/
    public class BaseController : Controller
    {
        protected readonly AccountApiService _accountApiService;
        protected readonly LookupApiService _lookupApiService;
        protected readonly TicketsApiService _ticketsApiService;
        protected string SessionId { set; get; }

        public BaseController()
        {
            _accountApiService = new AccountApiService();
            _lookupApiService = new LookupApiService();
            _ticketsApiService = new TicketsApiService();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            byte[] sessionBytes;
            // Trying to fetch AdminSessionId - to sessionBytes - check In Session if Admin is already logged in
            HttpContext.Session.TryGetValue("AdminSessionId", out sessionBytes);

            if (SessionRequired && (sessionBytes == null || sessionBytes.Length == 0))
            {
                RouteValueDictionary route = new RouteValueDictionary(new
                {
                    Controller = "Login",
                    Action = ""
                });

                // Go to Login Page (Session is valid from .NET Server so Admin is not already logged in)
                filterContext.Result = new RedirectToRouteResult(route);
                return;
            }
            else if(SessionRequired)
            {
                SessionId= Encoding.UTF8.GetString(sessionBytes); // GetSession 
                var result = _accountApiService.SessionCheck(new Models.API.AccountApi.SessionCheck.SessionCheckApiRequest
                {
                    SessionId = SessionId
                });
                if (result.ErrorCode != 0)
                {
                    // Session is Valid
                    RouteValueDictionary route = new RouteValueDictionary(new
                    {
                        Controller = "Login",
                        Action = ""
                    });

                    filterContext.Result = new RedirectToRouteResult(route);
                    return;
                }
            }
        }

        protected virtual bool SessionRequired { get { return true; } }

    }
}