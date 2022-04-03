using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;

namespace MinhaCarteira.Comum.Definicao.Interface.Servico
{
    public interface IUsuarioServico 
        : IServicoCrud<Usuario, IUsuarioRepositorio>
    {
    }
}
