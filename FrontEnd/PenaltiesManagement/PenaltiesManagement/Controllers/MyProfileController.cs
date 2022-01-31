using Microsoft.AspNetCore.Mvc;

namespace PenaltiesManagement.Controllers
{
    public class MyProfileController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}