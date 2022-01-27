using GrupoCard_Jokenpo.Models;

namespace GrupoCard_Jokenpo.Services
{
    public class Game : IGame
    {
        private Dictionary<string, Jokenpo> round = new Dictionary<string, Jokenpo>();

        public void Play(string token, Jokenpo move)
        {
            round[token] = move;
        }

        public void Undo(string token)
        {
            round.Remove(token);
        }

        public void Reset()
        {
            round.Clear();
        }

        public bool MadeAMove(string token)
        {
            return round.ContainsKey(token);
        }

        private int WinnerMove()
        {
            bool hpedra = (round.Where(game => game.Value == Jokenpo.Pedra).Count() > 0);
            bool htesoura = (round.Where(game => game.Value == Jokenpo.Tesoura).Count() > 0);
            bool hpapel = (round.Where(game => game.Value == Jokenpo.Papel).Count() > 0);

            if (hpedra && !hpapel)
                return (int)Jokenpo.Pedra;
            else if (hpapel && !htesoura)
                return (int)Jokenpo.Papel;
            else if (htesoura && !hpedra)
                return (int)Jokenpo.Tesoura;

            return -1;
        }

        public string[]? Winner()
        {
            Jokenpo winnermove = (Jokenpo)WinnerMove();

            if (winnermove >= 0)
            {
                return round.Where(bet => bet.Value == winnermove).Select(c => c.Key).ToArray();   
            }
            else
            {
                return null;
            }
        }
    }
}
