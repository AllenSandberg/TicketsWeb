namespace AdminApi.Models.Account.AdminUserCreate
{
    /** Wrapped in JSON Response - Indicating a Successful AdminUserCreate Operation. Newly created UserID is returned */

    public class AdminUserCreateResponse : BaseResponse
    {
        public int UserId { set; get; }
    }
}
