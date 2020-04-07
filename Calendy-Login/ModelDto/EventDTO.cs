using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Calendy_Login.Models;

namespace Calendy_Login.ModelDto
{
    public class EventDTO 
    {
        public Guid Id { get; set; }
        public Guid User_id { get; set; }
        public DateTime Start_time { get; set; }
        public DateTime End_time { get; set; }
        public string Event_type { get; set; }
    }
}