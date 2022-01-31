
namespace AdminApi.Models.Lookup.AppealTemplateFetchData
{
    /** Request to Fetch Specific AppealTemplate DATA WITH BLOB */
    public class AppealTemplateFetchDataRequest
    {
        public string SessionId { get; set; }
        public int RawId { get; set; }
    }
}
