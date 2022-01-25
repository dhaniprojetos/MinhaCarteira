using MinhaCarteira.Comum.Definicao.Interface.Entidade;

namespace MinhaCarteira.Comum.Definicao.Interface.Teste
{
    public interface IBuilder<TEntidade>
        where TEntidade: class, IEntidade
    {
        TEntidade DadosParaInsercao(params object[] args);
        TEntidade DadosParaAlteracao(TEntidade item);
    }
}
