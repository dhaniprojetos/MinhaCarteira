using System.Collections.Generic;
using System.ComponentModel;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using static System.Net.WebRequestMethods;

namespace MinhaCarteira.Comum.Definicao.Entidade
{
    public class Categoria : IEntidade
    {
        public int Id { get; set; }
        public int? IdAuxiliar { get; set; }
        [DisplayName("Categoria")]
        public string Nome { get; set; }
        public string Icone { get; set; }
        public string NomeArquivo { get; set; }

        public int? IdCategoriaPai { get; set; }
        public Categoria CategoriaPai { get; set; }
        public IList<Categoria> SubCategoria { get; set; }

        public string Caminho => GetCaminho(this);

        private string GetCaminho(Categoria obj)
        {
            return obj.CategoriaPai == null
                ? obj.Nome
                : $"{GetCaminho(obj.CategoriaPai)} \\ {obj.Nome}";
        }

        public string IconeCategoria => GetIconeCategoria(this);

        private string GetIconeCategoria(Categoria obj)
        {
            return !string.IsNullOrWhiteSpace(obj.Icone)
                ? obj.Icone
                : obj.CategoriaPai == null
                    ? "PD94bWwgdmVyc2lvbj0iMS4wIiA/PjxzdmcgZW5hYmxlLWJhY2tncm91bmQ9Im5ldyAwIDAgNjQgNjQiIGlkPSJMYXllcl8xIiB2ZXJzaW9uPSIxLjEiIHZpZXdCb3g9IjAgMCA2NCA2NCIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayI+PGNpcmNsZSBjeD0iMzIiIGN5PSIzMiIgZmlsbD0iIzc2QzJBRiIgcj0iMzIiLz48cGF0aCBkPSJNNDcuOTU1LDQyLjM5MmwtMi45ODMtOS41NDRjLTAuMTY1LTAuNTI3LTAuNzI4LTAuODE4LTEuMjUzLTAuNjU2bC04Ljg2NywyLjc3Mmw3LjA4My00LjQzOSAgYzAuNDY4LTAuMjkzLDAuNjA5LTAuOTEsMC4zMTYtMS4zNzhsLTYuOTEtMTEuMDI2Yy0wLjI5Mi0wLjQ2OC0wLjkxLTAuNjA5LTEuMzc4LTAuMzE2bC04Ljc0NCw1LjQ3OWwtMS4zMzYtNC4yNzQgIGMtMC42Ni0yLjExLTMuMDM3LTQuODEyLTYuODkxLTMuNjA5Yy0wLjc5MSwwLjI0Ny0xLjIzMSwxLjA4OC0wLjk4NCwxLjg3OWMwLjI0NywwLjc5MSwxLjA4OCwxLjIyOSwxLjg3OSwwLjk4NCAgYzIuMTc0LTAuNjgyLDIuOTUsMS4wOTMsMy4xMzIsMS42NGw3Ljk3LDI1LjQ5N2MtMi4wMDQsMS4zMTMtMi45OSwzLjg0MS0yLjI0MSw2LjI0YzAuNzIyLDIuMzA5LDIuODMxLDMuODYsNS4yNDksMy44NiAgYzAuNTU2LDAsMS4xMDgtMC4wODQsMS42NDItMC4yNTFjMS40MDItMC40MzgsMi41NS0xLjM5NiwzLjIzMS0yLjY5OGMwLjQ0Mi0wLjg0MywwLjYyOC0xLjc2NiwwLjYwMy0yLjY5Mmw4LjMzNS0yLjYwNSAgYzAuNzkxLTAuMjQ3LDEuMjMxLTEuMDg4LDAuOTg0LTEuODc5Yy0wLjI0OC0wLjc5Mi0xLjA5LTEuMjMyLTEuODc5LTAuOTg0bC04LjMyMywyLjYwMWwwLDBsMTAuNzA5LTMuMzQ3ICBjMC4yNTMtMC4wNzksMC40NjQtMC4yNTYsMC41ODctMC40OTFTNDguMDM0LDQyLjY0NSw0Ny45NTUsNDIuMzkyeiBNMzAuMzU2LDM2LjM2OGMtMC4yNTMsMC4wNzktMC40NjQsMC4yNTYtMC41ODcsMC40OTEgIHMtMC4xNDgsMC41MDktMC4wNjksMC43NjJsMi4xNTMsNi44ODloMGwtNC41NDUtMTQuNTQxbDMuODQ0LDYuMTMzYzAuMDAzLDAuMDA1LDAuMDA5LDAuMDA4LDAuMDEzLDAuMDEzTDMwLjM1NiwzNi4zNjh6IiBmaWxsPSIjMjMxRjIwIiBvcGFjaXR5PSIwLjIiLz48cG9seWdvbiBmaWxsPSIjRjVDRjg3IiBwb2ludHM9IiAgMzEuOTk5LDMyLjU3IDMxLjk5OSwzMi41NyA0MS40MDMsMjYuNjc2IDQxLjQwMywyNi42NzYgMzQuNDkzLDE1LjY1IDM0LjQ5MywxNS42NSAyNS4wODgsMjEuNTQ0IDI1LjA4OCwyMS41NDQgIiBzdHJva2U9IiNGNUNGODciIHN0cm9rZS1saW5lY2FwPSJyb3VuZCIgc3Ryb2tlLWxpbmVqb2luPSJyb3VuZCIgc3Ryb2tlLW1pdGVybGltaXQ9IjEwIiBzdHJva2Utd2lkdGg9IjIiLz48cGF0aCBkPSIgIE00NS4zNiw0Mi44MjJsLTEzLjM2Miw0LjE3N0wyMi40NSwxNi40NTZjMCwwLTEuMTkzLTMuODE4LTUuMDExLTIuNjI0IiBmaWxsPSJub25lIiBzdHJva2U9IiNGRkZGRkYiIHN0cm9rZS1saW5lY2FwPSJyb3VuZCIgc3Ryb2tlLWxpbmVqb2luPSJyb3VuZCIgc3Ryb2tlLW1pdGVybGltaXQ9IjEwIiBzdHJva2Utd2lkdGg9IjMiLz48cG9seWdvbiBmaWxsPSIjRTA5OTVFIiBwb2ludHM9IiAgMzMuNjM4LDQzLjg2NyAzMy42MzgsNDMuODY3IDQ3LDM5LjY5IDQ3LDM5LjY5IDQ0LjAxNywzMC4xNDYgNDQuMDE3LDMwLjE0NiAzMC42NTQsMzQuMzIyIDMwLjY1NCwzNC4zMjIgIiBzdHJva2U9IiNFMDk5NUUiIHN0cm9rZS1saW5lY2FwPSJyb3VuZCIgc3Ryb2tlLWxpbmVqb2luPSJyb3VuZCIgc3Ryb2tlLW1pdGVybGltaXQ9IjEwIiBzdHJva2Utd2lkdGg9IjIiLz48ZWxsaXBzZSBjeD0iMzEuOTk3IiBjeT0iNDYuOTk5IiBmaWxsPSIjRjVDRjg3IiByeD0iNCIgcnk9IjQiIHN0cm9rZT0iIzRGNUQ3MyIgc3Ryb2tlLWxpbmVjYXA9InJvdW5kIiBzdHJva2UtbGluZWpvaW49InJvdW5kIiBzdHJva2UtbWl0ZXJsaW1pdD0iMTAiIHN0cm9rZS13aWR0aD0iMyIgdHJhbnNmb3JtPSJtYXRyaXgoMC45NTQ1IC0wLjI5ODMgMC4yOTgzIDAuOTU0NSAtMTIuNTY0NSAxMS42ODY0KSIvPjwvc3ZnPg=="
                : GetIconeCategoria(obj.CategoriaPai);
        }

        public string NomeArquivoCategoria => GetNomeArquivoCategoria(this);

        private string GetNomeArquivoCategoria(Categoria obj)
        {
            return !string.IsNullOrWhiteSpace(obj.NomeArquivo)
                ? obj.NomeArquivo
                : obj.CategoriaPai == null
                    ? string.Empty
                    : GetNomeArquivoCategoria(obj.CategoriaPai);
        }


        public Categoria()
        {
            SubCategoria = new List<Categoria>();
        }
    }
}
