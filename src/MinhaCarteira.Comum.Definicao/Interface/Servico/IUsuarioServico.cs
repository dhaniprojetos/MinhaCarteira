using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using MinhaCarteira.Comum.Definicao.Modelo;
using System.Threading.Tasks;

namespace MinhaCarteira.Comum.Definicao.Interface.Servico
{
    public interface IUsuarioServico 
        : IServicoCrud<Usuario, IUsuarioRepositorio>
    {
        Task<Resposta<UsuarioToken>> Login(UsuarioLogin userInfo);
        Task<Resposta<bool>> ArmazenarPreferenciaUsuario(string username, string chaveValor);
    }
}
