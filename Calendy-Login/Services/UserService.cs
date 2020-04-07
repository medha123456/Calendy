using Calendy_Login.ModelDto;
using Calendy_Login.Models;
using Calendy_Login.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendy_Login.Services
{
    public class UserService : IUserService
    {
        //private readonly IEventServices _eventServices;
        //public UserService(IEventServices eventServices)
        //{
        //    _eventServices = eventServices;
        //}
        public static List<User> userList = new List<User>();
        List<Events> events = new List<Events>();
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
        public void userListInitialization()
        {
            events.Add(new_event);
            userList.Add(new User {
                Id = new Guid("6634fb23-fea0-4034-82eb-4a4c65e905ad"),
                Username = "medhasmriti155@gmail.com",
                First_name = "medha",
                Last_name = "smriti",
                Password = "mEDHA",
                Events = events
            });
            userList.Add(new User
            {
                Id = new Guid("6634fb23-fea0-4034-82eb-4a4c65e905at"),
                Username = "medhasmriti156@gmail.com",
                First_name = "mea",
                Last_name = "sm",
                Password = "HELLO",
                Events = null
            });
        }
        public IEnumerable<User> GetAll()
        {
            return userList;
        }
        public void UpdateUserList(Events events, bool flag)
        {
            var user = userList.Where(x => x.Id == events.User_id).FirstOrDefault();
            //List<Events> event_List = new List<Events>(); event_List =  user.Events;
            if (flag == true)
            {
                userList.Remove(user);
                //userevent_List.Add(events);
                user.Events.Add(events);
                userList.Add(user);
            }
            else
            {
                userList.Remove(user);
                user.Events.Add(events);
                userList.Add(user);
            }
        }
        public async Task<User> GetUserById(Guid id)
        {
            return  userList.Where(x => x.Id == id).FirstOrDefault();
        }
        public async Task<User> GetUserByUsername(string username)
        {
            return userList.Where(x => x.Username == username).FirstOrDefault();
        }
        
        public async Task<User> PostUser(UserDTO user)
        {
            var existingUser = userList?.Where(x => x.Id == user.Id || x.Username == user.Username);
            if (existingUser.Count() != 0)
                return existingUser.FirstOrDefault();
            User new_User = new User
            {
                Id = user.Id,
                Username = user.Username,
                First_name = user.First_name,
                Last_name = user.Last_name,
                Password = user.Password,
                Events = new List<Events>()
            };
            userList.Add(new_User);
            return null; 
        }
        public async Task<int> DeleteUser(Guid id)
        {
            if (userList.Where(x => x.Id == id) != null)
            {
                var user =  userList.Where(x => x.Id == id).FirstOrDefault();
                //foreach(var e in user.Events)
                //{
                //    await _eventServices.DeleteEventByUserId(e.Id);
                //}                
                userList.Remove(user);
                return 0;
            }
            return 1;
        }
     }
}