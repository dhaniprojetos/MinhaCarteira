using System.Linq;

namespace MinhaCarteira.Comum.Definicao.Interface.Modelo.Base
{
    public interface IFiltro<TEntidade>
    {
        IQueryable<TEntidade> Filtrar(IQueryable<TEntidade> source);
    }
}
