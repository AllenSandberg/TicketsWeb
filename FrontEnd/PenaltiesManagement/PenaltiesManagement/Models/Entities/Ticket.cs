using System;

namespace PenaltiesManagement.Models.Entities
{
    public class TicketEntity
    {
        public int SystemTicketId { set; get; }
        public string DriverLicense { set; get; }
        public string Status { set; get; }
        public string PenaltyReportNumber { set; get; }
        public string Passport { set; get; }
        public string VehiclePlateNumber { set; get; }
        public DateTime Created { set; get; }
        public DateTime Updated { set; get; }
        public string VehileType { set; get; }
        public string Phone { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Comments { set; get; }
        public string LastComment { set; get; }
        public string DriverName { set; get; }
    }
}
