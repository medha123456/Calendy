using Calendy_Login.ModelDto;
using Calendy_Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Calendy_Login.Services.Interfaces
{
    public interface IEventServices
    {
        IEnumerable<Events> GetAll();
        Task<Events> GetEventById(Guid id);
        Task<List<Events>> GetEventByUserName(string username);
        Task<int> PostEvent(EventDTO events);
        Task<int> DeleteEventByUserId(Guid id);
        Task<int> DeleteEventById(Guid id);
    }
}