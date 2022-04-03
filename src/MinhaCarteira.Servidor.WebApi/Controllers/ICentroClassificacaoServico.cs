using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public interface ICentroClassificacaoServico 
        : IServicoCrud<CentroClassificacao, ICentroClassificacaoRepositorio>
    {
    }
}
