using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaCarteira.Cliente.AppWebMvc.Servico.Base
{
    public interface IServicoBase<TEntidade>
    {
        [Get("")]
        Task<Resposta<IList<TEntidade>>> Navegar();

        [Get("/{id}")]
        Task<Resposta<TEntidade>> ObterPorId(int id);

        [Post("")]
        Task<Resposta<IList<TEntidade>>> Incluir(IList<TEntidade> itens);

        [Put("")]
        Task<Resposta<IList<TEntidade>>> Alterar(IList<TEntidade> itens);

        [Delete("")]
        Task<Resposta<int>> Deletar(int id);

    }
}
