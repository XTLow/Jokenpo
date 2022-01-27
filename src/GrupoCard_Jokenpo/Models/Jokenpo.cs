using System.Text.Json.Serialization;

namespace GrupoCard_Jokenpo.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Jokenpo
    {
        Pedra,
        Papel,
        Tesoura
    }
}
