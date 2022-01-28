using Microsoft.AspNetCore.Http;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Cliente.AppWebMvc.Models
{
    public class InstituicaoFinanceiraViewModel : IEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IFormFile Icone { get; set; }
    }
}
