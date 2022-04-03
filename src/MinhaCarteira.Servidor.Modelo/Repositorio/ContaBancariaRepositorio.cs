using Microsoft.EntityFrameworkCore;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Modelo.Repositorio
{
    public class ContaBancariaRepositorio 
        : RepositorioBase<ContaBancaria>, IContaBancariaRepositorio
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

                item.ValorSaldoAtual = item.ValorSaldoInicial;
            });

            return await base.IncluirRange(itens);
        }

        public async Task<bool> AtualizarSaldoConta(string id)
        {
            var filtro = !string.IsNullOrWhiteSpace(id)
                ? $"where conta.id in ({id})"
                : string.Empty;

            var cmdSql = $@"
update ContaBancaria
   set ValorSaldoAtual = ValorSaldoInicial + tmp.Saldo
from ContaBancaria conta
inner join (
    select conta.Id
         , isnull(sum(case when mov.TipoMovimento = 0 then Valor * -1 else Valor end), 0) Saldo
    from ContaBancaria conta
    left join MovimentoBancario mov on conta.Id = mov.ContaBancariaId and mov.DataMovimento > conta.DataSaldoInicial
    {filtro}
    group by conta.Id
)tmp on tmp.Id = conta.Id
";

            var afetadas = await Contexto.Database.ExecuteSqlRawAsync(cmdSql);

            return await Task.FromResult(afetadas > 0);
        }

    }
}
