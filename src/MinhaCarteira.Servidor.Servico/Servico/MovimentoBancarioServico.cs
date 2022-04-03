using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.Controle.Servico.Base;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class MovimentoBancarioServico 
        : ServicoBase<MovimentoBancario, IMovimentoBancarioRepositorio>, 
        IMovimentoBancarioServico
    {
        public MovimentoBancarioServico(IMovimentoBancarioRepositorio repositorio) : base(repositorio)
        {
        }

        public async Task<Tuple<int, IList<MovimentoBancario>>> ObterMovimentosParaConciliacao(ICriterio criterio)
        {
            var itens = await Repositorio
                .ObterMovimentosParaConciliacao(criterio);

            return itens;
        }
    }
}
