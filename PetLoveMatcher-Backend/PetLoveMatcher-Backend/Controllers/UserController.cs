using DtoNetProject.DTO;
using DtoNetProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetLoveMatcher_Backend.Models;

namespace PetLoveMatcher_Backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService service)
        {
            _userService = service;
        }


        //[FromQuery],[FromBody],[FromRoute],     [FromHeader],[FromForm],[FromService]

        // GET api/users
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<User> GetAll()
        {
            return _userService.GetUsers();
        }


        // GET api/users/Guid

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetOne([FromRoute] string id)
        {
            User? user = _userService.GetUserByID(id);
            return user == null ? NotFound("User with given Guid not found!") : Ok(user);

            //var customers = db.AspNetUsers
            //      .Where(u => u.AspNetRoles.Any(r => r.Name == "Customer") &&
            //                  u.IsActivated == true && u.IsClosed == false &&
            //                  u.IsPaused == false && u.IsSuspended == false)
            //      .ToList();

        }




        //Put api/users/1  - i jos ima body JSON { firstName="hello",... }
        [HttpPut] //ako necemo da ga zovemo Put nego NekaMetoda
        [Route("{id}")]
        [Authorize(Roles = "Admin,User")]
        //[ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int id, [FromBody] User user)
        {   
            try
            {
                var userName = HttpContext.User.Identity.Name;
                if (user.UserName != userName)
                {
                    return BadRequest("User cannot change other users information");
                }

                _userService.UpdateUser(user);
                return Ok("User successfully updated!");
            }
            catch (Exception ex)
            {
                _userService.InsertUser(user);
                return Ok("User successfully created!");

                // Log the exception or take other necessary corrective action.
                //Console.WriteLine(ex.StackTrace);
                //return StatusCode(500, "USER PUT:An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        //[ValidateAntiForgeryToken]
        //[Authorize(Policy = "AdminOnly")]
        public IActionResult Delete([FromRoute] string id)
        {
            Console.WriteLine(id);
            try
            {
                _userService.DeleteUser(id);
                return Ok("User successfully deleted!");
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.StackTrace);
                //return StatusCode(500, "USER DELETE: An error occurred while processing your request.");
                return BadRequest("User with given id alredy doesnt exit!");
            }

        }

        [Authorize]
        [HttpGet]
        [Route("protected-resource")]
        public IActionResult GetProtectedResource()
        {
            var user = HttpContext.User.Identity.Name;
            return Ok($"Hello, {user}!");
        }

    }
}


