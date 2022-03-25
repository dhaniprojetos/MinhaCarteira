using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Cliente.Recursos.Models
{
    public class UsuarioViewModel : IEntidade
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Roles { get; set; }
        public string TokenAcesso { get; set; }
    }
}
