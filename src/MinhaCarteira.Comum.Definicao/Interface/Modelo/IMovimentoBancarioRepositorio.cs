using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace MinhaCarteira.Comum.Definicao.Interface.Modelo
{
    public interface IMovimentoBancarioRepositorio 
        : ICrud<MovimentoBancario>
    {
        Task<bool> ConciliarParcela(int id, string idMovimentos);
        Task<Tuple<int, IList<MovimentoBancario>>> ObterMovimentosParaConciliacao(ICriterio criterio);
    }
}
