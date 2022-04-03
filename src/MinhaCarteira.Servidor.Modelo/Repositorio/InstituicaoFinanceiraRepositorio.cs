using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio.Base;

namespace MinhaCarteira.Servidor.Modelo.Repositorio
{
    public class InstituicaoFinanceiraRepositorio 
        : RepositorioBase<InstituicaoFinanceira>, IInstituicaoFinanceiraRepositorio
    {
        public InstituicaoFinanceiraRepositorio(MinhaCarteiraContext contexto)
            : base(contexto) { }
    }
}
