using Newtonsoft.Json;

namespace MinhaCarteira.Comum.Definicao.Interface.Servico
{
    public interface IRespostaServico
    {
        [JsonProperty(Order = 0)]
        int? StatusCode { get; set; }
        
        [JsonProperty(Order = 1)]
        string Mensagem { get; set; }
        
        [JsonProperty(Order = 2)]
        bool BemSucedido { get; set; }

        [JsonProperty(Order = 3)]
        string MensagemErro { get; set; }

        //[JsonProperty(Order = 3)]
        //IList<Exception> Erros { get; set; }
    }
}
