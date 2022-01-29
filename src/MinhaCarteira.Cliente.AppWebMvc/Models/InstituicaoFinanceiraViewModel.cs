using Microsoft.AspNetCore.Http;
using MinhaCarteira.Cliente.AppWebMvc.Attributes;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Cliente.AppWebMvc.Models
{
    public class InstituicaoFinanceiraViewModel : IEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Icone { get; set; }

        public string PathImagens { get; set; }

        [MaxFileSize("UploadTamanhoMaximo")]
        [AllowedExtensions("UploadExtensaoPermitida")]
        public IFormFile IconeForm { get; set; }
    }
}
