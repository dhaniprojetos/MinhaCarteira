using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using MinhaCarteira.Cliente.Recursos.Attributes.Base;

namespace MinhaCarteira.Cliente.Recursos.Attributes
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
            var valor = CarregarValorConfiguracao("DefinicaoArquivos", chave, string.Empty);
            _extensions = valor?
                .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToArray();
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            if (value is not IFormFile file) 
                return ValidationResult.Success;

            var extension = Path.GetExtension(file.FileName);
            return !_extensions.Contains(extension?.ToLower()) 
                ? new ValidationResult(GetErrorMessage()) 
                : ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            var tipos = string.Join(", ", _extensions);
            return $"Tipos de arquivo permitido: {tipos}.";
        }
    }
}
