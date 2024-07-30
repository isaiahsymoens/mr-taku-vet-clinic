namespace MrTakuVetClinic.DTOs.User
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public int? UserTypeId { get; set; }
        public bool? Active { get; set; }
    }
}
