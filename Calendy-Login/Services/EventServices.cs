using Calendy_Login.ModelDto;
using Calendy_Login.Models;
using Calendy_Login.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Calendy_Login.Services
{
    public class EventServices : IEventServices
    {
        private readonly IUserService _userService;
        public EventServices(IUserService userService)
        {
            _userService = userService;
        }
        public static List<Events> eventsList = new List<Events>();
        Events new_event = new Events()
        {
            Id = new Guid("6634fb23-fea0-4034-82eb-4a4c65e905bd"),
            User_id = new Guid("6634fb23-fea0-4034-82eb-4a4c65e905ad"),
            Start_time = DateTime.Now,
            End_time = DateTime.Now.AddHours(1),
            Duration = TimeSpan.FromHours(1),
            Event_type = "Online",
            Status = "Active"
        };
        public void InitializeEvent()
        {
            eventsList.Add(new_event);
        }
        public IEnumerable<Events> GetAll()
        {
            return eventsList;
        }

        public async Task<Events> GetEventById(Guid id)
        {
            var events =  eventsList?.Where(x => x.Id == id);
            return events.FirstOrDefault();
        }

        public async Task<List<Events>> GetEventByUserName(string username)
        {
            var user = await _userService.GetUserByUsername(username);
            var events =  eventsList?.FindAll(x => x.User_id == user.Id);
            return events;
        }

        public async Task<int> PostEvent(EventDTO events)
        {
            var user = await _userService.GetUserById(events.User_id);
            if (user == null) return 1;
            List<Events> existingEvents = user.Events;
            List<Events> eventValidation = existingEvents?.Where(x => x.Start_time <= events.Start_time && x.End_time >= events.Start_time).ToList();
            if(eventValidation.Count()== 0)
            {
                Events newevents = new Events
                {
                    Id = Guid.NewGuid(),
                    User_id = events.User_id,
                    Start_time = events.Start_time,
                    End_time = events.End_time,
                    Duration = (TimeSpan)(events.End_time - events.Start_time),
                    Status = "Active",
                    Event_type = events.Event_type
                };
                eventsList.Add(newevents);
                _userService.UpdateUserList(newevents, true);
                return 0;
            }
            return eventValidation.Count();
        }
        public async Task<int> DeleteEventByUserId(Guid user_id)
        {
            if(eventsList?.Where(x=> x.User_id == user_id) != null)
            {
                eventsList?.RemoveAll(x => x.User_id == user_id);
                return 1;
            }
            return 0;
        }
        public async Task<int> DeleteEventById(Guid id)
        {
            var events = eventsList?.Where(x => x.Id == id).FirstOrDefault();
            if (events != null)
            {
                var user = await _userService.GetUserById(events.User_id);
                eventsList?.RemoveAll(x => x.Id == id);
                _userService.UpdateUserList(events, false);
                return 1;
            }
            return 0;
        }
    }
}