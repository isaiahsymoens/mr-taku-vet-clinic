using System.Collections.Generic;

namespace MrTakuVetClinic.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public bool Active { get; set; }
        public UserType UserType { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
