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
		 , sum(case when mov.TipoMovimento = 0 then Valor * -1 else Valor end) Saldo
	from MovimentoBancario mov
	inner join ContaBancaria conta on conta.Id = mov.ContaBancariaId
	where mov.DataMovimento > conta.DataSaldoInicial
	group by conta.Id
)tmp on tmp.Id = conta.Id
{filtro}
";

            var afetadas = await Contexto.Database.ExecuteSqlRawAsync(cmdSql);

            return await Task.FromResult(afetadas > 0);
        }

    }
}
