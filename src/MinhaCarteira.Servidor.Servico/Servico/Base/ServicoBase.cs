using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Controle.Servico.Base
{
    public class ServicoBase<TEntidade, TCrud> : IServicoCrud<TEntidade, TCrud>
        where TEntidade : class, IEntidade
        where TCrud : ICrud<TEntidade>
    {
        public int TotalRegistros { get; set; }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Repositorio.Dispose();
                }
            }
            this.disposed = true;
        }

        public TCrud Repositorio { get; }
        public ServicoBase(TCrud repositorio)
        {
            Repositorio = repositorio;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> Deletar(int id)
        {
            return await Repositorio.Deletar(id);
        }
        public virtual async Task<TEntidade> Alterar(TEntidade item)
        {
            return await Repositorio.Alterar(item);
        }
        public virtual async Task<TEntidade> Incluir(TEntidade item)
        {
            return await Repositorio.Incluir(item);
        }

        public async Task<int> DeletarRange(int[] ids)
        {
            return await Repositorio.DeletarRange(ids);
        }
        public async Task<IList<TEntidade>> AlterarRange(IList<TEntidade> itens)
        {
            return await Repositorio.AlterarRange(itens);
        }
        public async Task<Tuple<int, IList<TEntidade>>> Navegar(
            ICriterio criterio)
        {
            var itens = await Repositorio.Navegar(criterio);

            return itens;
        }
        public async Task<IList<TEntidade>> IncluirRange(IList<TEntidade> itens)
        {
            var retorno = new List<TEntidade>();

            foreach (var item in itens)
            {
                var itemDb = await Incluir(item);
                retorno.Add(itemDb);
            }
            
            return retorno;
        }

        public async Task<TEntidade> ObterPorId(int id)
        {
            return await Repositorio.ObterPorId(id);
        }
    }
}
