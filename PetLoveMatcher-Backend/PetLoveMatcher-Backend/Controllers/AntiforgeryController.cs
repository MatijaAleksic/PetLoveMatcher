using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProject.Controllers
{
    [Route("api/auth/antiforgery")]
    [ApiController]
    public class AntiforgeryController : Controller
    {
        private readonly IAntiforgery _antiforgery;

        public AntiforgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        [HttpGet]
        public IActionResult GetToken()
        {
            var tokens = _antiforgery.GetAndStoreTokens   (HttpContext);

            HttpContext.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken!,
                new CookieOptions { HttpOnly = false, Secure = true, SameSite = SameSiteMode.None });

            return Ok(
                new
                {
                    headerName = tokens.HeaderName,
                    token = tokens.RequestToken,
                }
            );
        }
    }
}
