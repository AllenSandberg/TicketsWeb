
namespace PenaltiesManagement.Models.System.AdminUserDetails
{
    public class AdminUserDetailsUpdateRequestModel
    {
        public string SessionId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Permissions { get; set; }
    }
}
