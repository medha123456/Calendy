using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calendy_Login.Models
{
    public class Events
    {
        public Guid Id { get; set; }
        public Guid User_id { get; set;}
        public DateTime Start_time { get; set; }
        public DateTime End_time { get; set; }
        public TimeSpan? Duration { get; set; }
        public string Status { get; set; }
        public string Event_type { get; set; }
    }
}