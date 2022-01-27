using GrupoCard_Jokenpo.Models;
using GrupoCard_Jokenpo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GrupoCard_Jokenpo.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGame _game;
        public GameController(IGame jogo)
        {
            _game = jogo;
        }

        /// <summary>
        /// Make a move
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]        
        [Route("[controller]/Play")]
        public IActionResult Play(Jokenpo move)
        {
            var token = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            _game.Play(token, move);
            return Ok();
        }

        /// <summary>
        /// Undo a move
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("[controller]/Undo")]
        public IActionResult Undo()
        {
            var token = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            _game.Undo(token);
            return Ok();
        }
    }
}
