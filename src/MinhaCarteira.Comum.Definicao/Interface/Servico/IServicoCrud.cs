using MinhaCarteira.Comum.Definicao.Interface.Modelo;

namespace MinhaCarteira.Comum.Definicao.Interface.Servico
{
    public interface IServicoCrud<TEntidade, TCriterio> : ICrud<TEntidade, ICriterio<TEntidade>>
    {
        ICrud<TEntidade, TCriterio> Repositorio { get; }
    }
}
