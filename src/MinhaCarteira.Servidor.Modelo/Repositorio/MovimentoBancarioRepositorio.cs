using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Helper;
using MinhaCarteira.Comum.Definicao.Modelo;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio.Base;

namespace MinhaCarteira.Servidor.Modelo.Repositorio
{
    public class MovimentoBancarioRepositorio : RepositorioBase<MovimentoBancario>
    {
        public MovimentoBancarioRepositorio(MinhaCarteiraContext contexto) : base(contexto)
        {
        }

        //protected override IQueryable<MovimentoBancario> AdicionarOrdenacao(IQueryable<MovimentoBancario> source)
        //{
        //    return source.OrderByDescending(o => o.DataMovimento).ThenBy(tb => tb.Id);
        //}

        protected override IQueryable<MovimentoBancario> AdicionarIncludes(IQueryable<MovimentoBancario> source)
        {
            return source
                .Include(i => i.CentroClassificacao)
                .Include(i => i.Pessoa)
                .Include(i => i.ContaBancaria)
                .Include(i => i.Categoria)
                    .ThenInclude(ti => ti.CategoriaPai)
                    .ThenInclude(ti => ti.CategoriaPai);
        }

        public async Task<IList<MovimentoBancario>> ObterMovimentosParaConciliacao()
        {
            var itens = await
                AdicionarIncludes(Contexto.MovimentosBancarios)
                .AsNoTracking()
                .Where(w => w.AgendamentoItemId == null)
                .ToListAsync();

            return itens;
        }

        public async Task<bool> ConciliarParcela(int id, string idMovimentos)
        {
            var cmdSql = $"update MovimentoBancario set AgendamentoItemId={id} where Id in ({idMovimentos.Remove(idMovimentos.Length - 1)})";
            var afetadas = await Contexto.Database.ExecuteSqlRawAsync(cmdSql);

            return await Task.FromResult(afetadas > 0);
        }

        private async Task<bool> AtualizarSaldoConta(MovimentoBancario movimento)
        {
            var valor = movimento.ValorReal
                .ToString(CultureInfo.CreateSpecificCulture("pt-BR"));
            valor = valor.Replace(".", string.Empty).Replace(",", ".");

            var cmdSql = @$"
update ContaBancaria
   set ValorSaldoAtual = ValorSaldoAtual + ({valor})
where Id={movimento.ContaBancariaId} and DataSaldoInicial < '{movimento.DataMovimento:yyyyMMdd HH:mm:ss}'
";
            var afetadas = await Contexto.Database.ExecuteSqlRawAsync(cmdSql);

            return await Task.FromResult(afetadas > 0);
        }

        public async override Task<IList<MovimentoBancario>> IncluirRange(IList<MovimentoBancario> itens)
        {
            var itensDb = await base.IncluirRange(itens);

            foreach (var item in itensDb)
                await AtualizarSaldoConta(item);

            return itensDb;
        }

        protected async override Task<IList<MovimentoBancario>> ExecutarAntesAlterar(IList<MovimentoBancario> itens)
        {
            try
            {
                for (int i = 0; i < itens.Count; i++)
                {
                    var item = await ObterPorId(itens[i].Id);
                    item.TipoMovimento = item.TipoMovimento == TipoMovimento.Debito
                        ? TipoMovimento.Credito
                        : TipoMovimento.Debito;

                    await AtualizarSaldoConta(item);

                    item.Mapear(itens[i]);
                    itens[i] = item;

                    await AtualizarSaldoConta(itens[i]);

                    Contexto.Entry(item).State = EntityState.Modified;
                }

                return itens;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async override Task<int> DeletarRange(int[] ids)
        {
            try
            {
                var objs = Contexto.MovimentosBancarios
                    .Where(w => ids.Contains(w.Id))
                    .ToList();

                foreach (var item in objs)
                {
                    item.TipoMovimento = item.TipoMovimento == TipoMovimento.Debito
                        ? TipoMovimento.Credito
                        : TipoMovimento.Debito;

                    await AtualizarSaldoConta(item);
                }

                Tabela.RemoveRange(objs);
                var retorno = await Contexto.SaveChangesAsync();
                return retorno;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
