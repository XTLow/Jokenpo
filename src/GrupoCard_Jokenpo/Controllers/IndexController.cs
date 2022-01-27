using GrupoCard_Jokenpo.Hubs;
using GrupoCard_Jokenpo.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace GrupoCard_Jokenpo.Controllers
{
    public class IndexController : Controller
    {
        private readonly IHubContext<PlayerHub> _playersHub;
        private readonly IPlayers _players;

        public IndexController(IHubContext<PlayerHub> hub, IPlayers players)
        {
            _playersHub = hub;
            _players = players;
        }

        /// <summary>
        /// Index page
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return View(_players.Players);
        }

        /// <summary>
        /// Sign In page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn()
        {
            await Logout();
            return View();
        }

        /// <summary>
        /// Sign In
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] string username)
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Super")
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
            {
                IsPersistent = true
            });
            
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Logout from current session
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
