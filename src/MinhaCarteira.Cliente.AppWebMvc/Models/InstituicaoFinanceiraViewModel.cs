using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using MinhaCarteira.Cliente.AppWebMvc.Attributes;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Cliente.AppWebMvc.Models
{
    public class InstituicaoFinanceiraViewModel : IEntidade
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
