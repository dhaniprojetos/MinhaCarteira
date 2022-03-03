using System.Collections.Generic;

namespace MinhaCarteira.Cliente.Recursos.Models
{
    public class ListaMovimentoBancarioViewModel
    {
        public ListaMovimentoBancarioViewModel()
        {
            Contas = new List<ContaBancariaViewModel>();
            Movimentos = new List<MovimentoBancarioViewModel>();
        }

        public IList<ContaBancariaViewModel> Contas { get; set; }
        public IList<MovimentoBancarioViewModel> Movimentos { get; set; }
    }
}
