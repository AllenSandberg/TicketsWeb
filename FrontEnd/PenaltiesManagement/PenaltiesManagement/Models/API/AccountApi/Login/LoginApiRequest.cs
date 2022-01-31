namespace PenaltiesManagement.Models.API.AccountApi.Login
{
    public class LoginApiRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ClientIp { get; set; }
        public string DeviceNumber { get; set; }
    }
}
