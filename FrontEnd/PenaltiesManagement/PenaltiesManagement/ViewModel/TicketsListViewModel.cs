using PenaltiesManagement.Models.Entities;
using System;
using System.Collections.Generic;

namespace PenaltiesManagement.ViewModel
{
    public class TicketsListViewModel
    {
        public PagerEntity Paging { set; get; }
        public List<TicketEntity> Tickets { get; set; }
        public TicketsSearchEntity TicketsSearch { get; set; }
    }

    public class TicketsSearchEntity
    {
        public string Passport { get; set; }
        public string VechilePlateNumber { get; set; }
        public string Ticketid { get; set; }
        public string Phone { get; set; }
        public DateTime RegisteredFrom { get; set; }
        public DateTime RegisteredTo { get; set; }
    }
}
