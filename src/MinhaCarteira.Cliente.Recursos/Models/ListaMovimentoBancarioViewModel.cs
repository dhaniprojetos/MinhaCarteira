using System.Collections.Generic;
using X.PagedList;

namespace MinhaCarteira.Cliente.Recursos.Models
{
    public class ListaMovimentoBancarioViewModel
    {
        public ListaMovimentoBancarioViewModel()
        {
            Contas = new List<ContaBancariaViewModel>();
            //Movimentos = new StaticPagedList<MovimentoBancarioViewModel>(null, 1, 1, 0);
        }

        public IList<ContaBancariaViewModel> Contas { get; set; }
        public IPagedList<MovimentoBancarioViewModel> Movimentos { get; set; }
    }
}