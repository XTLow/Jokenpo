using GrupoCard_Jokenpo.Services;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace GrupoCard_Jokenpo.Hubs
{
    public class PlayerHub : Hub
    {
        private readonly IPlayers _players;

        public PlayerHub(IPlayers players)
        {
            _players = players;
        }

        public async Task PlayerConnected(string player)
        {
            await Clients.Others.SendAsync("PlayerConnected", player);
        }

        public async Task PlayerDisconnected(string player)
        {
            await Clients.Others.SendAsync("PlayerDisconnected", player);
        }

        public override Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var newPlayer = new Models.Player { 
                    Name = Context.User.Identity.Name,
                    Token = Context.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value
                };

                _players.Players.Add(newPlayer);
                PlayerConnected(newPlayer.Name);
            }
            
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var player = _players.Players.Where(x => 
                {
                    return x.Token == Context.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
                }).FirstOrDefault();
                PlayerDisconnected(player.Name);
                _players.Players.Remove(player);
            }
            return base.OnDisconnectedAsync(exception);
        }        

    }
}
