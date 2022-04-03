using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;

namespace MinhaCarteira.Comum.Definicao.Interface.Servico
{
    public interface ICategoriaServico : IServicoCrud<Categoria, ICrud<Categoria>>
    {
    }
}
