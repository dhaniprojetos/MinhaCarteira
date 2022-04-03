using Microsoft.EntityFrameworkCore;
using MinhaCarteira.Comum.Definicao.Entidade.Relatorio;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Modelo.Data;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Modelo.Repositorio
{
    public class RelatorioRepositorio : IRelatorioRepositorio
    {
        private readonly MinhaCarteiraContext _contexto;

        public RelatorioRepositorio(MinhaCarteiraContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<ExtratoRelatorio> ObterRelatorioSaldos()
        {
            var cmdSql = @"
    drop table if exists ##extrato;
    drop table if exists ##extratoDiario;
    drop table if exists ##extratoMensal;
    
    select CAST(row_number() over(partition by grp.ContaBancariaId, grp.Data order by grp.Data) as int) Idx
         , grp.ContaBancariaId
         , CONVERT(DATE, grp.Data) Data
         , grp.Descricao
         , grp.Valor
         , sum(grp.Valor) over (partition by grp.ContaBancariaId order by grp.Data ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) Saldo
    into ##extrato
    from (
        select id
			 , datamovimento data
             , valor * case 
				    --when AgendamentoItemId is not null then 0
                    when TipoMovimento = 1 then 1 
                    when TipoMovimento = 0 then -1 
               end Valor
             , descricao
             , mov.ContaBancariaId
             , null ValorSaldo
        from movimentobancario mov

        union
        select ag.Id
		     , coalesce(agi.DataPagamento, agi.Data, ag.DataInicial) Data
             , coalesce(agi.ValorPago, agi.Valor, ag.Valor) * case 
				    when mov.Id is not null then 0
                    when ag.Tipo = 1 then 1
                    when ag.Tipo = 0 then -1
               end Valor
             , ag.descricao
             , coalesce(agi.ContaBancariaId, ag.ContaBancariaId) ContaBancariaId
             , null ValorSaldo
        from Agendamento ag
        inner join AgendamentoItem agi on agi.AgendamentoId = ag.Id
		left join MovimentoBancario mov on mov.AgendamentoItemId = agi.Id
        
        union
        select id
			 , DATEADD(day, -1, min(conta.Data)) Data
             , conta.ValorSaldoInicial + sum(isnull(conta.valor, 0)) Valor
             , 'SALDO INICIAL' descricao
             , conta.Id
             , null Saldo
        from (
            select conta.id
                 , coalesce(agi.DataPagamento, agi.Data, conta.DataSaldoInicial) Data
                 --, conta.DataSaldoInicial Data
                 , conta.valorsaldoinicial
                 , mov.valor * case when mov.TipoMovimento = 0 then 1 else -1 end Valor
            from ContaBancaria conta
            left join MovimentoBancario mov on mov.ContaBancariaId = conta.Id and mov.DataMovimento < conta.DataSaldoInicial
            left join AgendamentoItem agi on agi.ContaBancariaId = conta.Id and coalesce(agi.DataPagamento, agi.Data) < conta.DataSaldoInicial
            left join Agendamento ag on ag.Id = agi.AgendamentoId
        )conta
        group by conta.Id, conta.ValorSaldoInicial
    )grp;

    select cast(row_number() over (partition by ext.ContaBancariaId, substring(convert(nvarchar(8), ext.Data, 112), 1, 6) order by ext.ContaBancariaId, ext.Data) as int) idx
		 , ext.ContaBancariaId, ext.Data, ext.Descricao, ext.Valor, ext.Saldo
    into ##extratoDiario
    from ##extrato ext
    inner join (
        select max(Idx) Idx, ContaBancariaId, data
        from ##extrato
        group by ContaBancariaId, data
    )tmp on ext.Idx = tmp.Idx and ext.ContaBancariaId = tmp.ContaBancariaId and ext.Data = tmp.Data
    order by ext.ContaBancariaId, ext.Data;
    
	select ext.idx, ext.ContaBancariaId, grp.MesAno, ext.Descricao, ext.Valor, ext.Saldo
	into ##extratoMensal
	from ##extratoDiario ext
	inner join (
		select max(idx) idx
				, ContaBancariaId
				, substring(convert(nvarchar(8), Data, 112), 1, 6) MesAno
		from ##extratoDiario
		group by ContaBancariaId, substring(convert(nvarchar(8), Data, 112), 1, 6)
	) grp on grp.idx = ext.idx 
			and grp.contabancariaid = ext.contabancariaid 
			and grp.MesAno = substring(convert(nvarchar(8), ext.Data, 112), 1, 6)
	order by grp.ContaBancariaId, grp.MesAno
";
            using var ctx = _contexto;
            ctx.Database.OpenConnection();

            await ctx.Database.ExecuteSqlRawAsync(cmdSql);

            var extratoDiario = await ctx.ExtratoDiario
                .FromSqlRaw("select * from ##extratoDiario order by ContaBancariaId, Data")
                .ToListAsync();

            var extratoMensal = await ctx.ExtratoMensal
                .FromSqlRaw("select * from ##extratoMensal order by ContaBancariaId, MesAno")
                .ToListAsync();

            ctx.Database.CloseConnection();

            return new ExtratoRelatorio(extratoDiario, extratoMensal);
        }
    }
}
