using Microsoft.EntityFrameworkCore;
using MinhaCarteira.Comum.Definicao.Filtro;
using MinhaCarteira.Comum.Definicao.Helper;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Modelo;
using MinhaCarteira.Servidor.Modelo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Modelo.Repositorio.Base
{
    public abstract class RepositorioBase<TEntidade> : ICrud<TEntidade>
        where TEntidade : class, IEntidade
    {
        protected Expression<Func<TEntidade, bool>> SimpleComparison(IList<FiltroOpcao> queryFilterObjects)
        {
            //initialize the body expression
            BinaryExpression bodyExpression = null;
            BinaryExpression andExpressionBody = null;
            BinaryExpression orExpressionBody = null;

            //create parameter expression
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntidade), "DynamicFilterQuery");

            //list of binary expressions to store either the || or && operators
            List<BinaryExpression> andExpressions = new();
            List<BinaryExpression> orExpressions = new();

            foreach (var queryFilterObject in queryFilterObjects)
            {
                //create member property expression
                var property = Expression.Property(parameterExpression, queryFilterObject.NomePropriedade);

                //create the constant expression value
                //var constantExpressionValue = Expression.Constant(queryFilterObject.Valor, queryFilterObject.PropertyType);
                var valor = Convert.ChangeType(queryFilterObject.Valor, property.Type);
                var constantExpressionValue = Expression.Constant(valor, property.Type);

                //create the binary expression clause based on the comparison operator
                BinaryExpression clause = null;
                if (queryFilterObject.Operador == TipoOperadorBusca.Igual)
                {
                    clause = Expression.Equal(property, constantExpressionValue);
                }
                else if (queryFilterObject.Operador == TipoOperadorBusca.Diferente)
                {
                    clause = Expression.NotEqual(property, constantExpressionValue);
                }
                else if (queryFilterObject.Operador == TipoOperadorBusca.Maior)
                {
                    clause = Expression.GreaterThan(property, constantExpressionValue);
                }
                else if (queryFilterObject.Operador == TipoOperadorBusca.MaiorOuIgual)
                {
                    clause = Expression.GreaterThan(property, constantExpressionValue);
                }
                else if (queryFilterObject.Operador == TipoOperadorBusca.Menor)
                {
                    clause = Expression.LessThan(property, constantExpressionValue);
                }
                else if (queryFilterObject.Operador == TipoOperadorBusca.MenorOuIgual)
                {
                    clause = Expression.LessThanOrEqual(property, constantExpressionValue);
                }

                //you should validate against a null clause....

                //assign the item either to the relevant logical comparison expression list
                //if (queryFilterObject.LogicalOperator == "and" || queryFilterObject.LogicalOperator == "&&")
                //{
                //    andExpressions.Add(clause);
                //
                //}
                //else if (queryFilterObject.LogicalOperator == "or" || queryFilterObject.LogicalOperator == "||")
                //{
                //    orExpressions.Add(clause);
                //
                //}
                andExpressions.Add(clause);
            }

            if (andExpressions.Count > 0)
                andExpressionBody = andExpressions.Aggregate((e, next) => Expression.And(e, next));

            if (orExpressions.Count > 0)
                orExpressionBody = orExpressions.Aggregate((e, next) => Expression.Or(e, next));

            if (andExpressionBody != null && orExpressionBody != null)
                bodyExpression = Expression.OrElse(andExpressionBody, orExpressionBody);

            if (andExpressionBody != null && orExpressionBody == null)
                bodyExpression = andExpressionBody;

            if (andExpressionBody == null && orExpressionBody != null)
                bodyExpression = orExpressionBody;

            if (bodyExpression == null)
                throw new Exception("Null Expression.");

            var finalExpression = Expression.Lambda<Func<TEntidade, bool>>(bodyExpression, parameterExpression);

            return finalExpression;
        }

        protected virtual IQueryable<TEntidade> AdicionarIncludes(
            IQueryable<TEntidade> source)
        {
            return source;
        }
        protected virtual IQueryable<TEntidade> AdicionarOrdenacao(
            IQueryable<TEntidade> source)
        {
            return source;
        }
        protected virtual IQueryable<TEntidade> AdicionarFiltro(
            IQueryable<TEntidade> source,
            IList<FiltroOpcao> opcoesFiltro)
        {
            var filtro = SimpleComparison(opcoesFiltro);

            return source.Where(filtro);
        }
        protected virtual async Task<IList<TEntidade>> ExecutarAntesAlterar(
            IList<TEntidade> itens)
        {
            try
            {
                for (int i = 0; i < itens.Count; i++)
                {
                    var item = await ObterPorId(itens[i].Id);
                    item.Mapear(itens[i]);
                    itens[i] = item;

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

        public async Task<int> Deletar(int id)
        {
            return await DeletarRange(new[] { id });
        }
        public async Task<TEntidade> Alterar(TEntidade item)
        {
            var itens = await AlterarRange(new List<TEntidade> { item });
            return itens[0];
        }
        public async Task<TEntidade> Incluir(TEntidade item)
        {
            var itens = await IncluirRange(new List<TEntidade> { item });
            return itens[0];
        }
        public virtual async Task<int> DeletarRange(int[] ids)
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
        public virtual async Task<IList<TEntidade>> AlterarRange(IList<TEntidade> itens)
        {
            try
            {
                var itensPreparados = await ExecutarAntesAlterar(itens);
                if (itensPreparados == null) return null;
                Tabela.UpdateRange(itensPreparados);
                await Contexto.SaveChangesAsync();

                itens.ToList().ForEach(f =>
                    Contexto.Entry(f).State = EntityState.Detached);

                return itens;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public virtual async Task<IList<TEntidade>> IncluirRange(IList<TEntidade> itens)
        {
            try
            {
                await Tabela.AddRangeAsync(itens);
                await Contexto.SaveChangesAsync();

                itens.ToList().ForEach(f =>
                    Contexto.Entry(f).State = EntityState.Detached);

                return itens;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public virtual async Task<IList<TEntidade>> Navegar(
            ICriterio<TEntidade> criterio)
        {
            var tab = criterio.AdicionarIncludes
                ? AdicionarIncludes(Tabela).AsNoTracking()
                : Tabela.AsNoTracking();

            if (criterio != null && criterio.OpcoesFiltro.Any())
                tab = AdicionarFiltro(tab, criterio.OpcoesFiltro);

            tab = AdicionarOrdenacao(tab);

            var itens = await tab.ToListAsync();
            return itens;
        }

        public async Task<TEntidade> ObterPorId(int id)
        {
            var item = await AdicionarIncludes(Tabela)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (item != null)
                Contexto.Entry(item).State = EntityState.Detached;

            return item;
        }
    }
}
