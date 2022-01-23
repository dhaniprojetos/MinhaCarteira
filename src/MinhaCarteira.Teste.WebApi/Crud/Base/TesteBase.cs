using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Teste.Mock.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace MinhaCarteira.Teste.WebApi.Crud.Base
{
    public class TesteBase<TEntidade, TBuilder, TServico>
        where TEntidade : class, IEntidade
        where TBuilder : IBuilder<TEntidade>
        where TServico : class, IServicoCrud<TEntidade>
    {
        public TesteBase(
            IBuilder<TEntidade> builder, 
            IServicoCrud<TEntidade> servico,
            ITestOutputHelper output)
        {
            Builder = builder;
            Servico = servico;
            _output = output;
        }

        private  string NomeTipoGenerico => 
            typeof(TEntidade).Name;
        protected readonly ITestOutputHelper _output;
        protected IBuilder<TEntidade> Builder { get; }
        protected IServicoCrud<TEntidade> Servico { get; }

        protected virtual IList<TEntidade> GerarItens(int qtdItens)
        {
            var itens = new List<TEntidade>();
            for (int i = 0; i < qtdItens; i++)
                itens.Add(Builder.DadosParaInsercao(i));

            return itens;
        }
        protected virtual async Task<TEntidade[]> IncluirItensAsync(
            int qtdTestes)
        {
            Console.WriteLine(@"Inicializando a sequencia de inclusões");
            var itens = GerarItens(qtdTestes);
            var itensDb = await Servico.Incluir(itens);
            var ids = string.Join(
                ",", 
                itensDb.Select(s => s.Id).ToArray());

            _output.WriteLine($"ID gerados para {NomeTipoGenerico} : {ids}");

            return await Task.FromResult(itens.ToArray());
        }

    }
}
