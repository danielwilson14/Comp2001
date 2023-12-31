﻿namespace Comp2001.DTOs
{
    public class UserReadDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AboutMe { get; set; }
        public int LocationID { get; set; }
        public DateTime Birthday { get; set; }
        public bool Archived { get; set; }
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();

    }

    public class UserCreateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AboutMe { get; set; }
        public int LocationID { get; set; }
        public DateTime Birthday { get; set; }
        public string Password { get; set; }
    }
    public class UserUpdateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AboutMe { get; set; }
        public int LocationID { get; set; }
        public DateTime Birthday { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
    }
}
