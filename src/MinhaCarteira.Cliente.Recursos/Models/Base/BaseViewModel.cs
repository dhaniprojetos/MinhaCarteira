using MinhaCarteira.Comum.Definicao.Filtro;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using Newtonsoft.Json;

namespace MinhaCarteira.Cliente.Recursos.Models.Base
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            OpcaoAtual = OpcaoAtual == null ? new FiltroOpcao() : OpcaoAtual;
        }

        public bool PodeExibirPainelAvisoVazio => true;
        public ICriterio Filtro { get; set; }
        public string FiltroJson { get => JsonConvert.SerializeObject(Filtro); }
        public FiltroOpcao OpcaoAtual { get; set; }
    }
}
