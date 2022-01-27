using GrupoCard_Jokenpo.Models;

namespace GrupoCard_Jokenpo.Services
{
    public interface IGame
    {
        public void Play(string token, Jokenpo move);

        public void Undo(string token);

        public bool MadeAMove(string token);

        public string[] Winner();

        public void Reset();

    }
}
