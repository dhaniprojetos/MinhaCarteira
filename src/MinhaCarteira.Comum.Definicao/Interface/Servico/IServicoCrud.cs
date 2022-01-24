using MinhaCarteira.Comum.Definicao.Interface.Modelo;

namespace MinhaCarteira.Comum.Definicao.Interface.Servico
{
    public interface IServicoCrud<TEntidade> : ICrud<TEntidade>
    {
        ICrud<TEntidade> Repositorio { get; }
    }
}
