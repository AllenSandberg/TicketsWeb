using System;

namespace DataObjects.Entities
{
    public class Document
    {
        public int DocumentType { set; get; }
        public DateTime Uploaded { set; get; }
        public string DocumentPath { set; get; }
        public int DocumentId { set; get; }
    }
}
