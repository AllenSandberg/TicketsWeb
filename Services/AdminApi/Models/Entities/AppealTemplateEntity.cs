using System;

namespace AdminApi.Models.Entities
{
    public class AppealTemplateEntity
    {
        public string TemplateName { set; get; }
        public int TemplateId { set; get; }
        public int RawId { set; get; }
        public byte[] RawData { set; get; }
        public string ShortDescription { set; get; }
        public DateTime Created { set; get; }
        public DateTime Updated { set; get; }
        public string OriginalFileName { set; get; }
    }
}
