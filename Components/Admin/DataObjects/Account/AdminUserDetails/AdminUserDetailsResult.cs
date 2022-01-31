namespace DataObjects.Account.AdminUserDetails
{
    /** Output Parameter for AdminUserDetails(AdminUserDetailsParameters parameters). */

    public class AdminUserDetailsResult : BaseResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
