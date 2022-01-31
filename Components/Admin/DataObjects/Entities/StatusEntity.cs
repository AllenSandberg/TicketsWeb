namespace DataObjects.Entities
{
    /** Returned Result for GetStatuses(int statusId)
     * & Input Parameter for StatusAdd(StatusEntity status)*/
    public class StatusEntity
    {
        public int StatusId { set; get; }
        public string StatusName { set; get; }
        public string Description { set; get; }
    }
}
