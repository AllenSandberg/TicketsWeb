namespace AdminApi.Models.Account.Login
{
    /** Specified Object for AdminUserLoginRequest- This is passed to AccountManager's AccountDal */
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ClientIp { get; set; }
        public string DeviceIdentificator { get; set; }
    }
}
