
namespace AdminApi.Models.Entities
{
    /** Input Parameter for adding a Document to the DataBase. */
    public class DocumentType
    {
        public int Category { set; get; }
        public int DocumentTypeId { set; get; }
        public string DocumentTypeName { set; get; }
    }
}
