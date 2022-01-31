namespace PenaltiesManagement.Models.API.AccountApi.Registration
{
    public class AdminUserCreateApiRequest
    {
        public string SessionId { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string Permissions { set; get; }
    }
}
