using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.Controle.Servico;
using MinhaCarteira.Teste.Mock.Faker;
using MinhaCarteira.Teste.Mock.Interface;
using MinhaCarteira.Teste.WebApi.Crud.Base;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MinhaCarteira.Teste.WebApi.Crud
{
    public class PessoaTesteCrud : TesteBase<Pessoa, PessoaBuilder, PessoaServico>
    {
        public PessoaTesteCrud(
            IBuilder<Pessoa> builder,
            IServicoCrud<Pessoa> servico,
            ITestOutputHelper output) : base(builder, servico, output)
        { }

        [Fact]
        public async Task CadastrarPessoa()
        {
            var itens = await IncluirItensAsync(2);
        }

    }
}
