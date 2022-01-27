using GrupoCard_Jokenpo.Models;

namespace GrupoCard_Jokenpo.Services
{
    public class Players : IPlayers
    {
        private readonly List<Player> _players = new List<Player>();

        List<Player> IPlayers.Players { get => _players; }
    }
}
