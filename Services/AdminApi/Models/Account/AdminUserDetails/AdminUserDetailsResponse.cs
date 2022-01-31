namespace AdminApi.Models.Account.AdminUserDetails
{
    /** This will be wrapped in JSON - AdminFetch Response with Admin Details to the FrontEND */
    public class AdminUserDetailsResponse:BaseResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }
}
