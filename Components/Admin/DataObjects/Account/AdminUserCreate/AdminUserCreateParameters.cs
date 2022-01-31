
namespace DataObjects.Account.AdminUserCreate
{
    /** Input parameter for AdminUserCreate(AdminUserCreateParameters parameters) */
    public class AdminUserCreateParameters
    {
        public string SessionId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string KeyPassword { get; set; }
        public string Permissions { get; set; }
    }
}
