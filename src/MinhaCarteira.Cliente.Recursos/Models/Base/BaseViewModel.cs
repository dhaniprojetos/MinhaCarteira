using MinhaCarteira.Comum.Definicao.Filtro;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
using Newtonsoft.Json;

namespace MinhaCarteira.Cliente.Recursos.Models.Base
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            OpcaoAtual ??= new FiltroOpcao();
        }

        //public bool PodeExibirPainelAvisoVazio => true;
        public ICriterio Filtro { get; set; }
        public string FiltroJson { get => JsonConvert.SerializeObject(Filtro); }
        public FiltroOpcao OpcaoAtual { get; set; }
    }
}
