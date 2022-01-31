namespace PenaltiesManagement.Models.API.AccountApi.UserDetails
{
    public class UserDetailsApiResponse:BaseApiResponse
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int MerchantId { get; set; }
    }
}
