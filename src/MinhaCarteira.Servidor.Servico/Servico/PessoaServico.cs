using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.Controle.Servico.Base;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class PessoaServico 
        : ServicoBase<Pessoa, ICrud<Pessoa>>, IPessoaServico
    {
        public PessoaServico(ICrud<Pessoa> repositorio) : base(repositorio)
        {
        }
    }
}
