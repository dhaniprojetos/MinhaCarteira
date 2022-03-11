using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using MinhaCarteira.Cliente.Recursos.Attributes;
using MinhaCarteira.Cliente.Recursos.Models.Base;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Cliente.Recursos.Models
{
    public class InstituicaoFinanceiraViewModel : BaseViewModel, IEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Icone { get; set; }
        public string NomeArquivo { get; set; }
        public string MimeType
        {
            get
            {
                new FileExtensionContentTypeProvider()
                    .TryGetContentType(NomeArquivo, out string contentType);

                return contentType ?? "image/png";
            }
        }

        [MaxFileSize("UploadTamanhoMaximo")]
        [AllowedExtensions("UploadExtensaoPermitida")]
        public IFormFile IconeForm { get; set; }
    }
}
