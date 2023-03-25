using DtoNetProject.DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


using System.Security.Claims;
using System.Text;
using PetLoveMatcher_Backend.Models;
using DtoNetProject.Services;

namespace DtoNetProject.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        private readonly IConfiguration Configuration;

        private readonly UserService _userService;

        public AuthController(UserService service, SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration) 
        {
            _userService = service;

            _signInManager = signInManager;
            _userManager = userManager;
            Configuration = configuration;
        }



        [HttpPost("login")]
        [AllowAnonymous] //Ne treba autorizacija za ovaj endpoiint
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return Ok("Successful LOGIN!");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Unauthorized("Login failed! Bad credentials");
                }
            }
            return BadRequest("Something is wrong check it out!");
        }


        [HttpPost]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //var a = HttpContext.User.Claims
            return Ok("Logout!");
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new { Roles = roles });
        }


        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = new User(model.FirstName, model.LastName, model.UserName, model.Email, model.PhoneNumber);

            var result = await _userManager.CreateAsync(user);

            await _userManager.AddPasswordAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "User");

            if (!result.Succeeded)
            {

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            return Ok("User successfully registered!");
        }
    }
}
