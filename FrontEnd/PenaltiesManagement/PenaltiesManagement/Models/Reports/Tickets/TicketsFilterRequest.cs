using System;

namespace PenaltiesManagement.Models.Reports.Tickets
{
    public class TicketsFilterRequest
    {
        public DateTime RegisteredFrom { set; get; }
        public DateTime RegisteredTo { set; get; }
        public string Passport { set; get; }
        public string VechilePlateNumber { set; get; }
        public int PageNumber { set; get; }
        public string PenaltyReportNumber { set; get; }
        public string Phone { set; get; }
        public int hdBtnType { set; get; }
    }
}
