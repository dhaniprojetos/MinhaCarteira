﻿using MinhaCarteira.Comum.Definicao.Filtro;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using System.Collections.Generic;
using X.PagedList;

namespace MinhaCarteira.Cliente.Recursos.Models.Base
{
    public class ListaBaseViewModel<TEntidadeViewModel> where TEntidadeViewModel : BaseViewModel
    {
        public ListaBaseViewModel()
        {
            OpcaoAtual = OpcaoAtual == null ? new FiltroOpcao() : OpcaoAtual;

            Itens = new StaticPagedList<TEntidadeViewModel>(
                subset: new List<TEntidadeViewModel>(),
                pageNumber: 1,
                pageSize: 1,
                totalItemCount: 0);
        }
        public ListaBaseViewModel(IList<TEntidadeViewModel> itens, ICriterio filtro, int qtdItens)
            : this()
        {
            Itens = new StaticPagedList<TEntidadeViewModel>(
                subset: itens,
                pageNumber: filtro.Pagina,
                pageSize: filtro.ItensPorPagina,
                totalItemCount: qtdItens);
        }

        public IPagedList<TEntidadeViewModel> Itens { get; set; }
        public FiltroOpcao OpcaoAtual { get; set; }
    }
}