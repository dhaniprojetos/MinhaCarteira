using System;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using Newtonsoft.Json;

namespace MinhaCarteira.Comum.Definicao.Modelo.Servico
{
    public class Resposta<T> : IRespostaServico
    {
        public Resposta() { }

        public Resposta(T dados, string mensagem = null)
        {
            Dados = dados;
            Mensagem = mensagem;
            MensagemErro = dados is Exception 
                ? ObterExceptionMaisProfunda() 
                : string.Empty;
            BemSucedido = dados is not Exception;
            //Erros = null;
        }

        //private static string GetExceptionMessages(this Exception e, string msgs = "")
        //{
        //    if (e == null) return string.Empty;
        //    if (msgs == "") msgs = e.Message;
        //    if (e.InnerException != null)
        //        msgs += "\r\nInnerException: " + GetExceptionMessages(e.InnerException);
        //    return msgs;
        //}

        public string ObterExceptionMaisProfunda(Exception erro = null)
        {
            if (erro != null)
                return erro.InnerException == null
                    ? erro.Message
                    : ObterExceptionMaisProfunda(erro.InnerException);

            if (Dados is Exception ex)
                return ObterExceptionMaisProfunda(ex);

            return string.Empty;
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
        
        [JsonProperty(Order = 99)]
        public T Dados { get; set; }

        //[JsonProperty(Order = 3)]
        //public IList<Exception> Erros { get; set; }
        //public string[] MensagensErro
        //{
        //    get => Erros?
        //        .Select(s => s.Message)
        //        .ToArray();
        //}
    }
}
