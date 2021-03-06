using System;
using System.Collections.Generic;

namespace AdminApi.Models.Account.AdminUsersList
{
    public class AdminUsersListResponse:BaseResponse
    {
        public List<AdminUser> AdminUsers { get; set; }
    }
    public class AdminUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int CreatedByUser { get; set; }
        public string CretedByEmail { get; set; }
    }
}
