using DataObjects.Entities;

namespace DataObjects.Lookup.AppealTemplates
{
    /** Input Parameter for AppealTemplateAdd(AppealTemplatesAddParameters template) */
    public class AppealTemplateDeleteParameters
    {
        public int TemplateId { set; get; }
        public string SessionId { set; get; }
    }
}
