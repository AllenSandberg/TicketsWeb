namespace AdminApi.Models.Account.AdminUserDetails
{
    /** Input Parameter: UserDetails - [SesssionID, UserID]
     from FrontEND */
    public class AdminUserDetailsRequest
    {
        public string SessionId { get; set; }
        public int UserId { get; set; }
    }
}
