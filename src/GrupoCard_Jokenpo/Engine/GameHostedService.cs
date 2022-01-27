using GrupoCard_Jokenpo.Hubs;
using GrupoCard_Jokenpo.Services;
using Microsoft.AspNetCore.SignalR;

namespace GrupoCard_Jokenpo.Engine
{
    public class GameHostedService : IHostedService, IDisposable
    {
        private Timer _timer = null!;
        private readonly IGame _game;
        private readonly IHubContext<PlayerHub> _hub;
        private readonly IPlayers _players;
        public GameHostedService(IGame game, IPlayers players, IHubContext<PlayerHub> hub)
        {
            _game = game;
            _hub = hub;
            _players = players;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Execute, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Execute(object? estado)
        {
            var winners = _game.Winner();
            var result = _players.Players.Where(x => winners.Contains(x.Token));

            if (winners != null)
            {
                _hub.Clients.All.SendAsync("Result", string.Join(", ", result.Select(x => x.Name)));
            }
            _game.Reset();
        }
    }
}
