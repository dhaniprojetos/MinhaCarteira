using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.Controle.Servico.Base;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class CategoriaServico 
        : ServicoBase<Categoria, ICategoriaRepositorio>, ICategoriaServico
    {
        public CategoriaServico(ICategoriaRepositorio repositorio) 
            : base(repositorio)
        {
        }

        public async override Task<Categoria> Alterar(Categoria item)
        {
            var itemDb = await base.Alterar(item);
            itemDb.SubCategoria.Clear();

            return itemDb;
        }
    }
}
