using PenaltiesManagement.Models.Entities;
using System;
using System.Collections.Generic;

namespace PenaltiesManagement.ViewModel
{
    public class AdminUsersListViewModel
    {
        public List<AdminUser> AdminUsers { get; set; }
    }
    public class AdminUser : User
    {
        public int CretedByUserID { set; get; }
        public DateTime RegistrationDate { set; get; }
    }
}
