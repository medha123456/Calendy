using Calendy_Login.ModelDto;
using Calendy_Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Calendy_Login.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByUsername(string username);
        Task<User> PostUser(UserDTO user);
        Task<int> DeleteUser(Guid id);
        void UpdateUserList(Events newevents, bool v);
    }
}