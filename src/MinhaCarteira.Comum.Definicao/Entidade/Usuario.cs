using System.Text.Json.Serialization;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class Usuario : IEntidade
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public string Role { get; set; }
        [JsonIgnore]
        public string TokenAcesso { get; set; }
    }
}
