
using AdminApi.Models.Entities;

namespace AdminApi.Models.Lookup.AppealTemplateAdd
{
    //Add a new AppealTemplate with its BLOB Content - per Given User SessionID
    public class AppealTemplateAddRequest
    {
        public AppealTemplateEntity Template { set; get; }
        public string SessionId { set; get; }

    }
}
