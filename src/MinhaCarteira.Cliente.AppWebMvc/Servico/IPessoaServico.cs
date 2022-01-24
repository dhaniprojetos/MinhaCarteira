using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaCarteira.Cliente.AppWebMvc.Servico
{
    public interface IPessoaServico
    {
        [Get("/pessoa")]
        Task<Resposta<IList<Pessoa>>> Navegar();

        [Get("/pessoa/{id}")]
        Task<Resposta<Pessoa>> ObterPorId(int id);

        [Post("/pessoa")]
        Task<Resposta<IList<Pessoa>>> Incluir(IList<Pessoa> itens);

        [Put("/pessoa")]
        Task<Resposta<IList<Pessoa>>> Alterar(IList<Pessoa> itens);

        [Delete("/pessoa")]
        Task<Resposta<int>> Deletar(int id);
    }
}
