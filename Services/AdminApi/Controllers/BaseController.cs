using BuisnessLogic;
using Microsoft.AspNetCore.Mvc;

namespace AdminApi.Controllers
{
    /** Extends MVC's Controller. */
    [Produces("application/json")]
    [Route("api/v1/admin/[controller]")]
    public class BaseController : Controller
    {
        protected AccountManager _accountManager;
        protected LookupManager _lookupManager;
        protected TicketsManager _ticketsManager;
        public BaseController()
        {
            /** Run BusinessLogic's Managers:
             *  (1) AccountManager - All Account Managing Operations
                (2) LookupManager - All Search Operations
                (3) TicketsManager - All Tickets Operations. */
            _accountManager = new AccountManager();
            _lookupManager = new LookupManager();
            _ticketsManager = new TicketsManager();
        }
    }
}