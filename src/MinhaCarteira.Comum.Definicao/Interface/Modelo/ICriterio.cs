namespace MinhaCarteira.Comum.Definicao.Interface.Modelo
{
    public interface ICriterio<TEntidade>
    {
        //IFiltro<TEntidade> Filtro { get; set; }
        bool AdicionarIncludes { get; set; }
    }
}
