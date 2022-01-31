using System;

namespace PenaltiesManagement.Models.API.SystemApi.AppealTemplateFetch
{
    public class AppealTemplateFetchApiResponse
    {
        public string Filename { get; set; }
        public int TemplateId { get; set; }
        public int RawId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
