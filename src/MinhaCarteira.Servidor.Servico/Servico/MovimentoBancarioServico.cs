using System.Collections.Generic;
using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.Controle.Servico.Base;
using MinhaCarteira.Servidor.Modelo.Repositorio;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class MovimentoBancarioServico : ServicoBase<MovimentoBancario>, IMovimentoBancarioServico
    {
        public MovimentoBancarioServico(ICrud<MovimentoBancario> repositorio) : base(repositorio)
        {
        }

        public async Task<IList<MovimentoBancario>> ObterMovimentosParaConciliacao()
        {
            var itens = await((MovimentoBancarioRepositorio)Repositorio)
                .ObterMovimentosParaConciliacao();

            return itens;
        }
    }
}
