using Microsoft.EntityFrameworkCore;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Modelo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Modelo.Repositorio.Base
{
    public abstract class RepositorioBase<TEntidade> : ICrud<TEntidade>
        where TEntidade : class, IEntidade
    {
        protected virtual IQueryable<TEntidade> AdicionarIncludes(
            IQueryable<TEntidade> source)
        {
            return source;
        }
        protected virtual async Task<IList<TEntidade>> ExecutarAntesAlteracao(
            IList<TEntidade> itens)
        {
            try
            {
                for (int i = 0; i < itens.Count; i++)
                {
                    var item = await ObterPorId(itens[i].Id);
                    itens[i] = item;
                }

                return itens;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public DbSet<TEntidade> Tabela { get; }
        public MinhaCarteiraContext Contexto { get; }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Contexto.Dispose();
                }
            }
            this.disposed = true;
        }

        public RepositorioBase(MinhaCarteiraContext contexto)
        {
            Contexto = contexto;
            Tabela = contexto.Set<TEntidade>();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> Deletar(int[] ids)
        {
            try
            {
                var objs = Tabela.Where(w => ids.Contains(w.Id));

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
        public async Task<IList<TEntidade>> Alterar(IList<TEntidade> itens)
        {
            try
            {
                var itensPreparados = await ExecutarAntesAlteracao(itens);
                if (itensPreparados == null) return null;

                Tabela.UpdateRange(itensPreparados);
                await Contexto.SaveChangesAsync();

                return itens;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public async Task<IList<TEntidade>> Navegar(
            ICriterio<TEntidade> criterio)
        {
            var tab = criterio != null && criterio.AdicionarIncludes
                ? AdicionarIncludes(Tabela).AsNoTracking()
                : Tabela.AsNoTracking();

            if (criterio?.Filtro != null)
                tab = criterio.Filtro.Filtrar(tab);

            return await tab.ToListAsync();
        }
        public async Task<IList<TEntidade>> Incluir(IList<TEntidade> itens)
        {
            try
            {
                await Tabela.AddRangeAsync(itens);
                await Contexto.SaveChangesAsync();

                return itens;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<TEntidade> ObterPorId(int id)
        {
            var item = await AdicionarIncludes(Tabela)
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == id);

            Contexto.Entry(item).State = EntityState.Detached;

            return item;
        }
    }
}
