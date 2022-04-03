using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio.Base;
using System.Linq;
using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
using System;
using MinhaCarteira.Servidor.Modelo.Helper;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;

namespace MinhaCarteira.Servidor.Modelo.Repositorio
{
    public class AgendamentoRepositorio 
        : RepositorioBase<Agendamento>, IAgendamentoRepositorio
    {
        public AgendamentoRepositorio(MinhaCarteiraContext contexto)
            : base(contexto) { }

        public override async Task<IList<Agendamento>> IncluirRange(IList<Agendamento> itens)
        {
            itens.ToList().ForEach(item =>
            {
                if (item.CentroClassificacao?.Id > 0)
                    Contexto.Entry(item.CentroClassificacao).State =
                        EntityState.Unchanged;

                if (item.Pessoa?.Id > 0)
                    Contexto.Entry(item.Pessoa).State =
                        EntityState.Unchanged;

                if (item.Categoria?.Id > 0)
                    Contexto.Entry(item.Categoria).State =
                        EntityState.Unchanged;

                if (item.ContaBancaria?.Id > 0)
                    Contexto.Entry(item.ContaBancaria).State =
                        EntityState.Unchanged;
            });

            return await base.IncluirRange(itens);
        }

        protected override IQueryable<Agendamento> AdicionarIncludes(IQueryable<Agendamento> source)
        {
            return source
                .Include(i => i.Categoria)
                    .ThenInclude(ti => ti.CategoriaPai)
                    .ThenInclude(ti => ti.CategoriaPai)
                .Include(i => i.CentroClassificacao)
                .Include(i => i.Pessoa)
                .Include(i => i.ContaBancaria);
            //.Include(i => i.Items)
            //    .ThenInclude(ti => ti.Movimentos)
            //.Include(i => i.Items)
            //    .ThenInclude(ti => ti.Pessoa)
            //.Include(i => i.Items)
            //    .ThenInclude(ti => ti.ContaBancaria);
        }

        protected IQueryable<AgendamentoItem> AdicionarIncludesParcela(IQueryable<AgendamentoItem> source)
        {
            return source
                .Include(i => i.Pessoa)
                .Include(i => i.ContaBancaria)
                .Include(i => i.Agendamento)
                    .ThenInclude(ti => ti.CentroClassificacao)
                .Include(i => i.Agendamento)
                    .ThenInclude(ti => ti.Pessoa)
                .Include(i => i.Agendamento)
                    .ThenInclude(ti => ti.Categoria)
                        .ThenInclude(ti => ti.CategoriaPai)
                        .ThenInclude(ti => ti.CategoriaPai);
        }

        protected override async Task<IList<Agendamento>> ExecutarAntesAlterar(IList<Agendamento> itens)
        {
            var agendIds = itens.Select(s => s.Id).ToArray();
            var agendDb = await AdicionarIncludes(Contexto.Agendamentos)
                .AsNoTracking()
                .Where(w => agendIds.Contains(w.Id))
                .ToListAsync();

            foreach (var agend in itens)
            {
                agend.Items.ToList().ForEach(f =>
                {
                    var agendItemDb = agendDb
                        .SelectMany(s => s.Items, (_, agendItemDb) => agendItemDb)
                        .SingleOrDefault(w => w.Data == f.Data);

                    if (agendItemDb != null)
                        f.Id = agendItemDb.Id;
                });
            }

            var agendItemsIds = itens
                .SelectMany(s => s.Items, (_, i) => i?.Id)
                .Where(w => w > 0)
                .ToArray();

            var agendDbsItemsIds = agendDb
                .SelectMany(s => s.Items, (_, i) => (int)i?.Id)
                .Where(w => w > 0)
                .ToArray();

            var agendItemsRemovidos = agendDbsItemsIds
                .Where(w => !agendItemsIds.Contains(w))
                .ToArray();

            if (agendItemsRemovidos.Length <= 0)
                return itens;

            //await DeletarRange(agendItemsRemovidos);
            var ids = string.Join(",", agendItemsRemovidos);
            var cmdSql = $"delete from AgendamentoItem where id in ({ids});";
            await Contexto.Database.ExecuteSqlRawAsync(cmdSql);

            return itens;
        }

        public async Task<Tuple<int, IList<AgendamentoItem>>> ContasAVencer(ICriterio criterio)
        {
            var tab = criterio.AdicionarIncludes
                ? AdicionarIncludesParcela(Contexto.AgendamentoItens).AsNoTracking()
                : Contexto.AgendamentoItens.AsNoTracking();

            if (criterio != null && criterio.OpcoesFiltro.Any())
            {
                var filtro = SimpleComparison<AgendamentoItem>(criterio.OpcoesFiltro);

                tab = tab.Where(filtro);
            }

            var totalRegistros = await tab.CountAsync();

            if (!string.IsNullOrWhiteSpace(criterio.Ordenacao))
                tab = tab.OrderBy(criterio.Ordenacao);

            IList<AgendamentoItem> itens;

            if (criterio.ItensPorPagina <= 1)
                itens = await tab.ToListAsync();
            else itens = await tab
                    .Skip((criterio.Pagina - 1) * criterio.ItensPorPagina)
                    .Take(criterio.ItensPorPagina)
                    .ToListAsync();

            return new Tuple<int, IList<AgendamentoItem>>(totalRegistros, itens);


            //var itens = await 
            //    AdicionarIncludesParcela(Contexto.AgendamentoItens)
            //    .AsNoTracking()
            //    .Where(w => !w.EstahPaga && w.Data < System.DateTime.Now.AddDays(qtdDias) ||
            //                w.Data > System.DateTime.Now.AddDays(qtdDias * -1) && w.Data < System.DateTime.Now.AddDays(qtdDias))
            //    .OrderBy(o => o.Data)
            //    .ThenBy(tb => tb.ContaBancariaId)
            //    .ToListAsync();
            //
            //return itens;
        }

        public async Task<AgendamentoItem> ObterParcelaPorId(int id)
        {
            var item = await
                AdicionarIncludesParcela(Contexto.AgendamentoItens)
                .AsNoTracking()
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();

            return item;
        }

        public async Task<AgendamentoItem> BaixarParcela(AgendamentoItem parcela)
        {
            var itemDb = await ObterParcelaPorId(parcela.Id);
            itemDb.EstahPaga = true;
            itemDb.DataPagamento = parcela.Data;
            itemDb.ValorPago = parcela.Valor;
            itemDb.PessoaId = parcela.PessoaId;
            itemDb.ContaBancariaId = parcela.ContaBancariaId;

            Contexto.AgendamentoItens.Update(itemDb);
            await Contexto.SaveChangesAsync();

            Contexto.Entry(itemDb).State = EntityState.Detached;

            return parcela;
        }

        public async Task<bool> ConciliarParcela(int id)
        {
            var cmdSql = $"update AgendamentoItem set EstahConciliada=1 where Id={id}";
            var afetados = await Contexto.Database.ExecuteSqlRawAsync(cmdSql);

            return await Task.FromResult(afetados > 0);
        }
    }
}
