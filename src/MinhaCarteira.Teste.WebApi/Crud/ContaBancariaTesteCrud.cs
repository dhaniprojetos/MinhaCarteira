using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Teste.Mock.Faker;
using MinhaCarteira.Teste.WebApi.Crud.Base;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using MinhaCarteira.Teste.Mock.Interface;
using MinhaCarteira.Comum.Definicao.Filtro;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;

namespace MinhaCarteira.Teste.WebApi.Crud
{
    public class ContaBancariaTesteCrud : TesteBase<
        ContaBancaria,
        ContaBancariaBuilder,
        IContaBancariaServico,
        IContaBancariaRepositorio>
    {
        private readonly IInstituicaoFinanceiraServico _instituicaoFinanceiroServico;

        public ContaBancariaTesteCrud(
            IBuilder<ContaBancaria> builder,
            IContaBancariaServico servico,
            IInstituicaoFinanceiraServico instituicaoFinanceiroServico,
            ITestOutputHelper output)
            : base(builder, servico, output)
        {
            _instituicaoFinanceiroServico = instituicaoFinanceiroServico;
        }

        [Fact]
        public async Task TestarMetodosCrud()
        {
            await Task.Delay(2 * 1000);
            var retorno = await _instituicaoFinanceiroServico.Navegar(new FiltroBase());
            var bancos = retorno.Item2;

            var qtdTeste = 4;
            var itens = await IncluirItensAsync(qtdTeste, bancos);
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
