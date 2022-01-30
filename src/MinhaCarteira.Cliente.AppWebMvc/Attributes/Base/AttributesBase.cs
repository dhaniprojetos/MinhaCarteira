using System;
using System.ComponentModel.DataAnnotations;

namespace MinhaCarteira.Cliente.AppWebMvc.Attributes.Base
{
    public class AttributesBase : ValidationAttribute
    {
        protected T CarregarValorConfiguracao<T>(
            string secao,
            string chave,
            T valorPadrao)
        {
            if (secao == null) secao = "DefinicaoArquivos";

            var cfgSecao = Startup.Configuration.GetSection(secao);
            if (cfgSecao != null)
            {
                var valor = cfgSecao[chave];
                if (valor == null)
                    return valorPadrao;

                try
                {
                    return (T)Convert.ChangeType(valor, typeof(T));
                }
                catch
                {
                    return valorPadrao;
                }
            }

            return valorPadrao;
        }
    }
}
