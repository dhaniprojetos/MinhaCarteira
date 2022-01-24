using System;
using System.Linq;

namespace MinhaCarteira.Comum.Definicao.Modelo.Servico
{
    public class Resposta<T>
    {
        public Resposta() { }

        public Resposta(T dados)
        {
            Dados = dados;
            Mensagem = string.Empty;
            BemSucedido = false;
            Erros = null;
        }

        public T Dados { get; set; }
        public string Mensagem { get; set; }
        public bool BemSucedido { get; set; }
        public Exception[] Erros { get; set; }
        //public string[] MensagensErro
        //{
        //    get => Erros?
        //        .Select(s => s.Message)
        //        .ToArray();
        //}
    }
}
