using DataObjects.Entities;

namespace DataObjects.Lookup.AppealTemplates
{
    /** Input Parameter for AppealTemplateUpdate(AppealTemplatesUpdateParameters template)
     * */
    public class AppealTemplatesUpdateParameters
    {
        public AppealTemplateEntity Template { set; get; }
        public string SessionId { set; get; }
    }
}
