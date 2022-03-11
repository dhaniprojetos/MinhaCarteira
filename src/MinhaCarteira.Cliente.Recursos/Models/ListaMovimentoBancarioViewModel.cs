using MinhaCarteira.Cliente.Recursos.Models.Base;
using System.Collections.Generic;
using X.PagedList;

namespace MinhaCarteira.Cliente.Recursos.Models
{
    public class ListaMovimentoBancarioViewModel : BaseViewModel
    {
        public ListaMovimentoBancarioViewModel()
        {
            Contas = new List<ContaBancariaViewModel>();
            Movimentos = new PagedList<MovimentoBancarioViewModel>(
                null, 1, 1);
        }

        public IList<ContaBancariaViewModel> Contas { get; set; }
        public IPagedList<MovimentoBancarioViewModel> Movimentos { get; set; }
    }
}