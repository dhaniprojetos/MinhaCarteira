﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using Refit;

namespace MinhaCarteira.Cliente.Recursos.Refit.Base
{
    public interface IServicoBase<TEntidade>
    {
        [Get("")]
        Task<Resposta<IList<TEntidade>>> Navegar();

        [Get("/{id}")]
        Task<Resposta<TEntidade>> ObterPorId(int id);

        [Post("")]
        Task<Resposta<TEntidade>> Incluir(TEntidade item);

        [Put("")]
        Task<Resposta<TEntidade>> Alterar(TEntidade item);

        [Delete("")]
        Task<Resposta<int>> Deletar(int id);
    }
}