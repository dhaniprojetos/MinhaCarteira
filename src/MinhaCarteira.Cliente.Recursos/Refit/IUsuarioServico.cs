using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Modelo;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Refit;

namespace MinhaCarteira.Cliente.Recursos.Refit
{
    public interface IUsuarioServico
    {
        [Post("/login")]
        Task<Resposta<UsuarioToken>> Logar(UsuarioLogin item);
    }
}
