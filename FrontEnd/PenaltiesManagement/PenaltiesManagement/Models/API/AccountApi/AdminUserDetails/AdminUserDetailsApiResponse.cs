
namespace PenaltiesManagement.Models.API.AccountApi.AdminUserDetails
{
    public class AdminUserDetailsApiResponse:BaseApiResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }
}
