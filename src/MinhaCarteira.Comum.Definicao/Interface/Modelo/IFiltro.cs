using System.Linq;

namespace MinhaCarteira.Comum.Definicao.Interface.Modelo
{
    public interface IFiltro<TEntidade>
    {
        IQueryable<TEntidade> Filtrar(IQueryable<TEntidade> source);
    }
}
