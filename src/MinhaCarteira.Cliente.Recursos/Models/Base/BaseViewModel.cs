using MinhaCarteira.Comum.Definicao.Filtro;

namespace MinhaCarteira.Cliente.Recursos.Models.Base
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            OpcaoAtual = OpcaoAtual == null ? new FiltroOpcao() : OpcaoAtual;
        }

        public bool PodeExibirPainelAvisoVazio => true;
        public FiltroBase Filtro { get; set; }
        public FiltroOpcao OpcaoAtual { get; set; }
    }
}
