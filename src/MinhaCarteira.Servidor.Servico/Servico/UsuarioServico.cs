using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Controle.Servico.Base;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class UsuarioServico : ServicoBase<Usuario, ICrud<Usuario>>
    {
        public UsuarioServico(ICrud<Usuario> repositorio) : base(repositorio)
        {
        }
    }
}
