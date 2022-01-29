using Microsoft.AspNetCore.Http;
using MinhaCarteira.Cliente.AppWebMvc.Attributes.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace MinhaCarteira.Cliente.AppWebMvc.Attributes
{
    public class AllowedExtensionsAttribute : AttributesBase
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }
        public AllowedExtensionsAttribute(string chave)
        {
            var valor = CarregarValorConfiguracao(null, chave, string.Empty);
            _extensions = valor?
                .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToArray();
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            var tipos = string.Join(", ", _extensions);
            return $"Tipos de arquivo permitido: {tipos}.";
        }
    }
}
