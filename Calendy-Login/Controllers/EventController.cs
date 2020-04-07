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
using Microsoft.AspNetCore.Http;

namespace Calendy_Login.Controllers
{
   // [Route("api/[controller]")]
    [RoutePrefix("api/event")]
    //[Authorize]
    public class EventController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IEventServices _eventServices;

        public EventController(IUserService userService, IEventServices eventServices)
        {
            _eventServices = eventServices;
            _userService = userService;
        }

        [HttpGet]
        [Route("")]
        // GET api/events
        public IEnumerable<Events> GetAllEvent()
        {
            //var user_Login =  _userService.GetUserByUsername(userLogin.Username);
            //if (user_Login.P != userLogin.Password)
            //    return Unauthorized();
            if (_eventServices.GetAll() == null)
                return null;
            return _eventServices.GetAll();
        }
        [HttpGet]
        [Route("{id}")]
        // GET api/events/id
        public async Task<IHttpActionResult> GetEventById(Guid id, [FromUri]UserLogin userLogin)
        {
            var user_Login = await _userService.GetUserByUsername(userLogin.Username);
            if (user_Login.Password != userLogin.Password)
                return Unauthorized();
            var event_byid = await _eventServices.GetEventById(id);
            if (event_byid == null)
                return BadRequest("No such event");
            return Ok(event_byid);
        }
        [HttpGet]
        [Route("username/{username}")]
        // GET api/events/username
        public async Task<IHttpActionResult> GetEventByUser(string username,[FromUri] UserLogin userLogin)
        {
            var user_Login = await _userService.GetUserByUsername(userLogin.Username);
            if (user_Login.Password != userLogin.Password)
                return Unauthorized();
            var eventByUsername = await _eventServices.GetEventByUserName(username);
            if (eventByUsername == null)
                return BadRequest("No such event");
            return Ok(eventByUsername);
        }
        [HttpPost]
        [Route("")]
        // POST api/events/event
        public async Task<IHttpActionResult> PostEvent(EventDTO events)
        {
            var eventValidation = await _eventServices.PostEvent(events);
            if (eventValidation == 0)
                return Ok(HttpStatusCode.Created);
            return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        // DELETE api/events/id
        public async Task<IHttpActionResult> DeleteEventById(Guid id, UserLogin userLogin)
        {
            var user_Login = await _userService.GetUserByUsername(userLogin.Username);
            if (user_Login.Password != userLogin.Password)
                return Unauthorized();
            var eventValidation = await _eventServices.DeleteEventById(id);
            if (eventValidation != 0)
                return Ok(HttpStatusCode.OK);
            return BadRequest();
        }
    }
}
