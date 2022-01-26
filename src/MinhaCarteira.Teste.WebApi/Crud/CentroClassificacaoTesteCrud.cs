﻿using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Comum.Definicao.Interface.Teste;
using MinhaCarteira.Servidor.Controle.Servico;
using MinhaCarteira.Teste.Mock.Faker;
using MinhaCarteira.Teste.WebApi.Crud.Base;
using Xunit;
using Xunit.Abstractions;

namespace MinhaCarteira.Teste.WebApi.Crud
{
    public class CentroClassificacaoTesteCrud : TesteBase<
        CentroClassificacao,
        CentroClassificacaoBuilder,
        CentroClassificacaoServico>
    {
        public CentroClassificacaoTesteCrud(
            IBuilder<CentroClassificacao> builder,
            IServicoCrud<CentroClassificacao> servico,
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
