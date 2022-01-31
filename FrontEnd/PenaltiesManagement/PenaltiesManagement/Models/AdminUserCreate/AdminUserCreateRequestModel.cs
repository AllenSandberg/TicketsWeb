namespace PenaltiesManagement.Models.AdminUserCreate
{
    public class AdminUserCreateRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Permissions { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}