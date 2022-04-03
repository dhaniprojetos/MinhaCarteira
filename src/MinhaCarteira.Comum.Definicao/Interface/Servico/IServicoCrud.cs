using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;

namespace MinhaCarteira.Comum.Definicao.Interface.Servico
{
    public interface IServicoCrud<TEntidade, TCrud> : ICrud<TEntidade>
        where TEntidade: class, IEntidade
        where TCrud : ICrud<TEntidade>
    {
        TCrud Repositorio { get; }
    }
}
