namespace DataObjects.Account.AdminUserUpdate
{
    /** Input Parameter for AdminUserUpdate(AdminUserUpdateParameters parameters) */
    public class AdminUserUpdateParameters
    {
        public string SessionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }
        public string Phone { get; set; }
        public string Permissions { get; set; }
        public string Password { get; set; }
    }
}
