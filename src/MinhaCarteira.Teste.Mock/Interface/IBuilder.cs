using Bogus;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Teste.Mock.Interface
{
    public interface IBuilder<TEntidade>
        where TEntidade : class, IEntidade
    {
        Faker<TEntidade> DadosParaInsercao(params object[] args);
        TEntidade DadosParaAlteracao(TEntidade item);
    }
}
