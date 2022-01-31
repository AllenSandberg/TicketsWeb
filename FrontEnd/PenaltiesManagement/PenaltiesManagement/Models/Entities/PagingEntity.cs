namespace PenaltiesManagement.Models.Entities
{
    public class PagingEntity
    {
        public int PageNumber { set; get; }
        public int TotalPages { set; get; }
        public bool PrevioousLinkAvailable
        {
            get
            {
                return PageNumber == 1 ? false : true;
            }

        }
        public bool NextLinkAvailable
        {
            get
            {
                return PageNumber == TotalPages ? false : true;
            }

        }
    }
}
