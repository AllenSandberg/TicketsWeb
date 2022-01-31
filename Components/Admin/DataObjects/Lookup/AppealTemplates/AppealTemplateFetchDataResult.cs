
namespace DataObjects.Lookup.AppealTemplates
{
    /** Returned Result for AppealTemplateFetchDataResult */
    public class AppealTemplateFetchDataResult:BaseResult
    {
        public string FileName { set; get; }
        public byte[] RawData { get; set; }
    }
}
