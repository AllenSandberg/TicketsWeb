using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PenaltiesManagement.Models;
using System.Threading.Tasks;
using PenaltiesManagement.ViewModel;
using PenaltiesManagement.Models;

namespace PenaltiesManagement.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            byte[] nameBytes;
            HttpContext.Session.TryGetValue("AdminUserName", out nameBytes);
            if(nameBytes==null || nameBytes.Length == 0)
            {
                var userDetailsResult = await _accountApiService.AdminUserDetails(new PenaltiesManagement.Models.API.AccountApi.AdminUserDetails.AdminUserDetailsApiRequest
                {
                    SessionId = SessionId,
                    UserId=0
                });

                HttpContext.Session.SetString("AdminUserName", $"{userDetailsResult.LastName + userDetailsResult.FirstName}");
            }

            var viewModel = new DashboardViewModel();

            return View(viewModel);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
