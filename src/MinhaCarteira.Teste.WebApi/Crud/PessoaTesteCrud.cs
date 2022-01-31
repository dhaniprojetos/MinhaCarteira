using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.Controle.Servico;
using MinhaCarteira.Teste.Mock.Faker;
using MinhaCarteira.Teste.WebApi.Crud.Base;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using MinhaCarteira.Teste.Mock.Interface;

namespace MinhaCarteira.Teste.WebApi.Crud
{
    public class PessoaTesteCrud : TesteBase<
        Pessoa, 
        PessoaBuilder, 
        PessoaServico>
    {
        public PessoaTesteCrud(
            IBuilder<Pessoa> builder,
            IServicoCrud<Pessoa> servico,
            ITestOutputHelper output)
            : base(builder, servico, output) { }

        [Fact]
        public async Task TestarMetodosCrud()
        {
            var qtdTeste = 4;
            var itens = await IncluirItensAsync(qtdTeste);
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
