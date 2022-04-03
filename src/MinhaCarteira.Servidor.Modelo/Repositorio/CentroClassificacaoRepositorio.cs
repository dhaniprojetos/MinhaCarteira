using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio.Base;

namespace MinhaCarteira.Servidor.Modelo.Repositorio
{
    public class CentroClassificacaoRepositorio 
        : RepositorioBase<CentroClassificacao>, ICentroClassificacaoRepositorio
    {
        public CentroClassificacaoRepositorio(MinhaCarteiraContext contexto) 
            : base(contexto)
        {
        }
    }
}
