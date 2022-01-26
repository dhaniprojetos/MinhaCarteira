using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Controle.Servico.Base;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class MovimentoBancarioServico : ServicoBase<MovimentoBancario>
    {
        public MovimentoBancarioServico(ICrud<MovimentoBancario> repositorio) : base(repositorio)
        {
        }
    }
}
