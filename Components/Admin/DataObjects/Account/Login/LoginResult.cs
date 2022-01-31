namespace DataObjects.Account.Login
{
    /** Output Parameter for LoginResult Login(LoginParameters parameters) */
    public class LoginResult:BaseResult
    {
        public string SessionId { get; set; }
    }
}
