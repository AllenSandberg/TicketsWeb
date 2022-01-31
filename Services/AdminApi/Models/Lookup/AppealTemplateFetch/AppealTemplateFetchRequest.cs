
namespace AdminApi.Models.Lookup.AppealTemplateFetch
{
    // Provide SessionID & TemplateID for fetching AppealTemplateDetails (Without Blob)
    public class AppealTemplateFetchRequest
    {
        public string SessionId { get; set; }
        public int TemplateId { get; set; }

    }
}
