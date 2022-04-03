using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;

namespace MinhaCarteira.Comum.Definicao.Interface.Servico
{
    public interface IMovimentoBancarioServico : IServicoCrud<MovimentoBancario, ICrud<MovimentoBancario>>
    {
        Task<Tuple<int, IList<MovimentoBancario>>> ObterMovimentosParaConciliacao(ICriterio criterio);
    }
}
