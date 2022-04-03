using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;

namespace MinhaCarteira.Comum.Definicao.Interface.Servico
{
    public interface IMovimentoBancarioServico 
        : IServicoCrud<MovimentoBancario, IMovimentoBancarioRepositorio>
    {
        Task<Tuple<int, IList<MovimentoBancario>>> ObterMovimentosParaConciliacao(ICriterio criterio);
    }
}
