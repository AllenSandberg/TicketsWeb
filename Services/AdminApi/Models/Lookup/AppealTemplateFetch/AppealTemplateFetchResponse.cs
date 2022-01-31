using System;

namespace AdminApi.Models.Lookup.AppealTemplateFetch
{
    public class AppealTemplateFetchResponse:BaseResponse
    {
        public string Filename { get; set; }
        public int TemplateId { get; set; }
        public int RawId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}
