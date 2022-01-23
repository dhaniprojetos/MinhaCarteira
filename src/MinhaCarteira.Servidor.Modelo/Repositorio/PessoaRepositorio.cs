using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio.Base;

namespace MinhaCarteira.Servidor.Modelo.Repositorio
{
    public class PessoaRepositorio : RepositorioBase<Pessoa>
    {
        public PessoaRepositorio(MinhaCarteiraContext contexto) : base(contexto)
        {
        }
    }
}
