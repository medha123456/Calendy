using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calendy_Login.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Password { get; set; }
        public List<Events> Events { get; set; }
    }
}