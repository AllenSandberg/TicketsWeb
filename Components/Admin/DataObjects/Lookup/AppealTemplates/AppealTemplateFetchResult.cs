using System;

namespace DataObjects.Lookup.AppealTemplates
{
    /** Returned Result for AppealTemplateFetch(AppealTemplateFetchParameters parameters) */
    public class AppealTemplateFetchResult:BaseResult
    {
        public string Filename { get; set; }
        public int TemplateId { get; set; }
        public int RawId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
