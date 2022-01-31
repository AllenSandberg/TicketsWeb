using DataObjects.Entities;

namespace DataObjects.Lookup.AppealTemplates
{
    /** Input Parameter for AppealTemplateAdd(AppealTemplatesAddParameters template) */
    public class AppealTemplatesAddParameters
    {
        public AppealTemplateEntity Template { set; get; }
        public string SessionId { set; get; }
    }
}
