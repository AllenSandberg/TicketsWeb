
namespace DataObjects.Entities
{
    /**  Returned Result for GetClientDocumentsTypes(int category)
     * & Input Parameter DocumentsTypeAdd(DocumentType documentType)
     * & Input Parameter DocumentsTypeUpdate(DocumentType documentType)
     */
    public class DocumentType
    {
        public int Category { set; get; }
        public int DocumentTypeId { set; get; }
        public string DocumentTypeName { set; get; }
    }
}
