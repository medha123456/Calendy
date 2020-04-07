using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Calendy_Login.ModelDto;
using Calendy_Login.Models;
using Calendy_Login.Services.Interfaces;

namespace Calendy_Login.Controllers
{
    //[Route("api/[controller]")]
    [RoutePrefix("api/User")]
    //[Authorize]
    public class UserController : ApiController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        [Route("{id}")]
        // GET api/User/id
        public async Task<IHttpActionResult> GetUserDetails(Guid id, [FromUri]UserLogin userLogin)
        {
            var user_Login = await userService.GetUserByUsername(userLogin.Username);
            if (user_Login.Password != userLogin.Password)
                return Unauthorized();
            var user = await userService.GetUserById(id);
            if (user == null)
                return BadRequest("No such User");
            return Ok(user);
        }
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        // POST api/User
        public async Task<IHttpActionResult> RegisterNewUser(UserDTO user)
        {
            if (user == null) return BadRequest("No user");
            var user_Validation = await  userService.PostUser(user);
            if (user_Validation == null)
                return Ok(HttpStatusCode.Created);
            return BadRequest("User Already Exists");
        }
        [HttpDelete]
        [Route("{id}")]
        // DELETE api/User/id
        public async Task<IHttpActionResult> DeleteUser(Guid id, UserLogin userLogin)
        {
            var user_Login = await userService.GetUserByUsername(userLogin.Username);
            if (user_Login.Password != userLogin.Password)
                return Unauthorized();
            var user_Validation = await userService.DeleteUser(id);
            if (user_Validation == 0)
                return Ok(HttpStatusCode.Accepted);
            return BadRequest("User Doesn't Exists");
        }
    }
}
