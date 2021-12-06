using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace reserreadingauth.common
{
    public class Account
    {
        [Key]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}