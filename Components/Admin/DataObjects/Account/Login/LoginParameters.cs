namespace DataObjects.Account.Login
{
    /** Input Parameter for LoginResult Login(LoginParameters parameters) */
    public class LoginParameters
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ClientIp { get; set; }
        public string DeviceNumber { get; set; }
    }
}
