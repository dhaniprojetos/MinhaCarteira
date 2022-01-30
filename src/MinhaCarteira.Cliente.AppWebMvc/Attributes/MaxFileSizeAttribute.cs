using Microsoft.AspNetCore.Http;
using MinhaCarteira.Cliente.AppWebMvc.Attributes.Base;
using System.ComponentModel.DataAnnotations;

namespace MinhaCarteira.Cliente.AppWebMvc.Attributes
{
    public class MaxFileSizeAttribute : AttributesBase
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }
        public MaxFileSizeAttribute(string chave)
        {
            _maxFileSize = CarregarValorConfiguracao(null, chave, 0);
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Tamanho maximo permitido é de { _maxFileSize} bytes.";
        }
    }
}
