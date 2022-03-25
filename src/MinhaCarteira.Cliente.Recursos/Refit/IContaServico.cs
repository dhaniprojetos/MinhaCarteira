using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Refit;

namespace MinhaCarteira.Cliente.Recursos.Refit
{
    public interface IContaServico
    {
        [Post("/login")]
        Task<Resposta<UserToken>> Logar(UserInfo item);
    }
}
