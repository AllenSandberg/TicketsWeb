namespace DataObjects.Account.AdminUserDetails
{
    /** Each Admin has :
     *  (1) String SessionId
     *  (2) Integer UserID
     *  
     *  Input Parameter for AdminUserDetails(AdminUserDetailsParameters parameters).
     * */
    public class AdminUserDetailsParameters
    {
        public string SessionId { get; set; }
        public int UserId { get; set; }
    }
}

