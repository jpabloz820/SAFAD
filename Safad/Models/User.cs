﻿namespace Safad.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
