namespace AdminApi.Models.Account.AdminUserUpdate
{
    /* AdminUserDetails to be Updated - from FrontEND & All specific AdminUserDetails to find reference */
    public class AdminUserUpdateRequest
    {
        public string SessionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }
        public string Phone { get; set; }
        public string Permissions { get; set; }

    }
}
