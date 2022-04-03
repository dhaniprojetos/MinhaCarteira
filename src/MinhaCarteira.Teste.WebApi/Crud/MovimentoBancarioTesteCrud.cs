using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Filtro;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.Controle.Servico;
using MinhaCarteira.Teste.Mock.Faker;
using MinhaCarteira.Teste.Mock.Interface;
using MinhaCarteira.Teste.WebApi.Crud.Base;
using Xunit;
using Xunit.Abstractions;

namespace MinhaCarteira.Teste.WebApi.Crud
{
    public class MovimentoBancarioTesteCrud : TesteBase<
        MovimentoBancario,
        MovimentoBancarioBuilder,
        MovimentoBancarioServico>
    {
        private readonly ICentroClassificacaoServico _centroClassificacaoServico;
        private readonly IPessoaServico _pessoaServico;
        private readonly ICategoriaServico _categoriaServico;
        private readonly IContaBancariaServico _contaBancariaServico;


        public MovimentoBancarioTesteCrud(
            IBuilder<MovimentoBancario> builder,
            IMovimentoBancarioServico servico,
            ITestOutputHelper output,
            IContaBancariaServico contaBancariaServico,
            ICentroClassificacaoServico centroClassificacaoServico,
            IPessoaServico pessoaServico,
            ICategoriaServico categoriaServico)
            : base(builder, servico, output)
        {
            _centroClassificacaoServico = centroClassificacaoServico;
            _pessoaServico = pessoaServico;
            _categoriaServico = categoriaServico;
            _contaBancariaServico = contaBancariaServico;
        }

        [Fact]
        public async Task TestarMetodosCrud()
        {
            await Task.Delay(4 * 1000);
            var filtro = new FiltroBase();
            var retornoCentrosClassificacao = await _centroClassificacaoServico.Navegar(filtro);
            var centros = retornoCentrosClassificacao.Item2;

            var retornoPessoa = await _pessoaServico.Navegar(filtro);
            var pessoas = retornoPessoa.Item2;

            var retornoCategoria = await _categoriaServico.Navegar(filtro);
            var categorias = retornoCategoria.Item2;

            var retornoContas = await _contaBancariaServico.Navegar(filtro);
            var contas = retornoContas.Item2;

            var qtdTeste = 20;
            var itens = await IncluirItensAsync(qtdTeste, centros, pessoas, categorias, contas);
            var qtdItensAdicionados = itens.Length;
            Assert.Equal(qtdTeste, qtdItensAdicionados);

            itens = await AlterarIntesAsync(itens);
            var qtdItensAlterados = itens.Length;
            Assert.Equal(qtdItensAdicionados, qtdItensAlterados);

            //var removidos = await DeletarAsync(itens);
            //Assert.Equal(qtdItensAlterados, removidos);
        }

    }
}
