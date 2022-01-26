using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Controle.Servico.Base;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class PessoaServico : ServicoBase<Pessoa>
    {
        public PessoaServico(ICrud<Pessoa> repositorio) : base(repositorio)
        {
        }
    }
}
