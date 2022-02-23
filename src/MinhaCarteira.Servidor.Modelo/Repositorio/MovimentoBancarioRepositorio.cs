using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio.Base;

namespace MinhaCarteira.Servidor.Modelo.Repositorio
{
    public class MovimentoBancarioRepositorio : RepositorioBase<MovimentoBancario>
    {
        public MovimentoBancarioRepositorio(MinhaCarteiraContext contexto) : base(contexto)
        {
        }

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

        public async Task<bool> ConciliarParcela(int id, string idMovimentos)
        {
            var cmdSql = $"update MovimentoBancario set AgendamentoItemId={id} where Id in ({idMovimentos.Remove(idMovimentos.Length - 1)})";
            var afetadas = await Contexto.Database.ExecuteSqlRawAsync(cmdSql);

            return await Task.FromResult(afetadas > 0);
        }
    }
}
