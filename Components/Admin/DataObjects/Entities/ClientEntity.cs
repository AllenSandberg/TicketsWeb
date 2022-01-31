using System;


namespace DataObjects.Entities
{
    public class ClientEntity
    {
        public string Email { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public DateTime ResgitrationDate { set; get; }
        public int Status { set; get; }
        public int ClientId { set; get; }

    }
}
