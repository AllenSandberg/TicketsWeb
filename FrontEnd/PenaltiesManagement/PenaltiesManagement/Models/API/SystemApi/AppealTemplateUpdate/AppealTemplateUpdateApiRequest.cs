
using PenaltiesManagement.Models.Entities;

namespace PenaltiesManagement.Models.API.SystemApi.AppealTemplateUpdate
{
    public class AppealTemplateUpdateApiRequest
    {
        public AppealTemplateEntity Template { set; get; }
        public string SessionId { set; get; }
    }
}
