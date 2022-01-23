﻿using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Controle.Servico.Base
{
    public class ServicoBase<TEntidade> : IServicoCrud<TEntidade>
        where TEntidade : class, IEntidade
    {
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

        public ICrud<TEntidade> Repositorio { get; }
        public ServicoBase(ICrud<TEntidade> repositorio)
        {
            Repositorio = repositorio;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> Deletar(int[] ids)
        {
            return await Repositorio.Deletar(ids);
        }
        public async Task<IList<TEntidade>> Alterar(IList<TEntidade> itens)
        {
            return await Repositorio.Alterar(itens);
        }
        public async Task<IList<TEntidade>> Navegar(
            ICriterio<TEntidade> criterio)
        {
            return await Repositorio.Navegar(criterio);
        }
        public async Task<IList<TEntidade>> Incluir(IList<TEntidade> itens)
        {
            return await Repositorio.Incluir(itens);
        }

        public async Task<TEntidade> ObterPorId(int id)
        {
            return await Repositorio.ObterPorId(id);
        }
    }
}
