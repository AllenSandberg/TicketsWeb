
using PenaltiesManagement.Models.Entities;

namespace PenaltiesManagement.Models.API.SystemApi.AppealTemplateAdd
{
    public class AppealTemplateAddApiRequest
    {
        public AppealTemplateEntity Template { set; get; }
        public string SessionId { set; get; }
    }
}
