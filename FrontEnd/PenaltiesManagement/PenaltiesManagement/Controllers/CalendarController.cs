using Microsoft.AspNetCore.Mvc;

namespace PenaltiesManagement.Controllers
{
    public class CalendarController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}