using System.Net.Http;

namespace AdminApi.APIServices
{
    public class BaseApiClient
    {
        // Object for Sending HTTP Requests & Recieving HTTP Responses
        #region Fields
        protected readonly HttpClient _client;
        #endregion

        #region Ctor
        public BaseApiClient()
        {
            _client = new HttpClient();
        }
        #endregion

    }
}
