namespace DataObjects.Lookup.AppealTemplates
{
    /** Input Parameters for AppealTemplateFetch(AppealTemplateFetchParameters parameters) */
    public class AppealTemplateFetchParameters
    {
        public string SessionId { get; set; }
        public int TemplateId { get; set; }
    }
}
