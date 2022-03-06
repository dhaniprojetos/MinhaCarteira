using System;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using Newtonsoft.Json;

namespace MinhaCarteira.Comum.Definicao.Modelo.Servico
{
    public class Resposta<T> : IRespostaServico
    {
        private string ObterExceptionMaisProfunda(Exception erro = null)
        {
            if (erro != null)
                return erro.InnerException == null
                    ? erro.Message
                    : ObterExceptionMaisProfunda(erro.InnerException);

            if (Dados is Exception ex)
                return ObterExceptionMaisProfunda(ex);

            return string.Empty;
        }

        public Resposta() { }

        public Resposta(T dados, string mensagem = null)
        {
            Dados = dados;
            Mensagem = mensagem;
            MensagemErro = dados is Exception 
                ? ObterExceptionMaisProfunda() 
                : string.Empty;
            BemSucedido = dados is not Exception;
        }

        //Propriedades precisam ter GET e SET públicos para a deserialização do cliente
        [JsonProperty(Order = 0)]
        public int? StatusCode { get; set; }
        
        [JsonProperty(Order = 1)]
        public string Mensagem { get; set; }
        
        [JsonProperty(Order = 2)]
        public bool BemSucedido { get; set; }
        
        [JsonProperty(Order = 2)]
        public string MensagemErro { get; set; }

        [JsonProperty(Order = 3)]
        public int TotalRegistros { get; set; }

        [JsonProperty(Order = 99)]
        public T Dados { get; set; }
    }
}
