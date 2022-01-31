namespace AdminApi.Models.Account.AdminUserCreate
{
    public class AdminUserCreateRequest
    {
        public string SessionId { set; get; }
        public string FirstName { set; get; }
        public string Permissions { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
    }
}
