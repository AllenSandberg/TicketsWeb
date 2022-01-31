
using AdminApi.Models.Entities;

namespace AdminApi.Models.Lookup.AppealTemplateUpdate
{
    public class AppealTemplateUpdateRequest
    {
        public AppealTemplateEntity Template { set; get; }
        public string SessionId { set; get; }
    }
}
