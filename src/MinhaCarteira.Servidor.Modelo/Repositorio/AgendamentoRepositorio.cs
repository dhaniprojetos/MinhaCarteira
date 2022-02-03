using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio.Base;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Modelo.Repositorio
{
    public class AgendamentoRepositorio : RepositorioBase<Agendamento>
    {
        public AgendamentoRepositorio(MinhaCarteiraContext contexto)
            : base(contexto) { }

        protected override IQueryable<Agendamento> AdicionarIncludes(IQueryable<Agendamento> source)
        {
            return source
                .Include(i => i.Categoria)
                    .ThenInclude(ti => ti.CategoriaPai)
                    .ThenInclude(ti => ti.CategoriaPai)
                .Include(i => i.CentroClassificacao)
                .Include(i => i.Pessoa)
                .Include(i => i.ContaBancaria)
                .Include(i => i.Items)
                    .ThenInclude(ti => ti.Movimentos)
                .Include(i => i.Items)
                    .ThenInclude(ti => ti.Pessoa)
                .Include(i => i.Items)
                    .ThenInclude(ti => ti.ContaBancaria);
        }

        protected override async Task<IList<Agendamento>> ExecutarAntesAlterar(IList<Agendamento> itens)
        {
            var ids = itens.Select(s => s.Id).ToArray();
            var itemIds = itens
                .SelectMany(s => s.Items, (_, i) => i?.Id)
                .Where(w => w > 0)
                .ToArray();

            var itensDb = await AdicionarIncludes(Contexto.Agendamentos)
                .AsNoTracking()
                .Where(w => ids.Contains(w.Id))
                .ToListAsync();


            return await base.ExecutarAntesAlterar(itens);
        }
    }
}
