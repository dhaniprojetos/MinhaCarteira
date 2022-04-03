using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.Controle.Servico.Base;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class CategoriaServico 
        : ServicoBase<Categoria, ICrud<Categoria>>, ICategoriaServico
    {
        public CategoriaServico(ICrud<Categoria> repositorio) : base(repositorio)
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
