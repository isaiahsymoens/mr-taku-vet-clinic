﻿namespace MrTakuVetClinic.DTOs.User
{
    public class UserDto
    {
        public string FirstName {  get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string UserType { get; set; }
        public bool Active { get; set; }
    }
}
