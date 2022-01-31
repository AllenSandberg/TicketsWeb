
namespace AdminApi.Models.Lookup.AppealTemplateDelete
{
    //Add a new AppealTemplate with its BLOB Content - per Given User SessionID
    public class AppealTemplateDeleteRequest
    {
        public int TemplateId { set; get; }
        public string SessionId { set; get; }

    }
}
