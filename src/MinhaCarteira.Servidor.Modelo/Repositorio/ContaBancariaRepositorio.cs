using Microsoft.EntityFrameworkCore;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Modelo.Repositorio
{
    public class ContaBancariaRepositorio : RepositorioBase<ContaBancaria>
    {
        public ContaBancariaRepositorio(MinhaCarteiraContext contexto) : base(contexto)
        {
        }

        protected override IQueryable<ContaBancaria> AdicionarIncludes(IQueryable<ContaBancaria> source)
        {
            return source.Include(i => i.InstituicaoFinanceira);
        }

        public override async Task<IList<ContaBancaria>> IncluirRange(
            IList<ContaBancaria> itens)
        {
            itens.ToList().ForEach(item =>
            {
                if (item.InstituicaoFinanceira?.Id > 0)
                    Contexto.Entry(item.InstituicaoFinanceira).State =
                        EntityState.Unchanged;
            });

            return await base.IncluirRange(itens);
        }

    }
}
