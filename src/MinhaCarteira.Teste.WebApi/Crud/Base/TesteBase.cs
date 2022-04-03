using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit.Abstractions;
using MinhaCarteira.Teste.Mock.Interface;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;

namespace MinhaCarteira.Teste.WebApi.Crud.Base
{
    public class TesteBase<TEntidade, TBuilder, TServico>
        where TEntidade : class, IEntidade
        where TBuilder : IBuilder<TEntidade>
        where TServico : IServicoCrud<TEntidade, ICrud<TEntidade>>
    {
        public TesteBase(
            IBuilder<TEntidade> builder,
            IServicoCrud<TEntidade, ICrud<TEntidade>> servico,
            ITestOutputHelper output)
        {
            Builder = builder;
            Servico = servico;
            _output = output;
        }

        private static string NomeTipoGenerico =>
            typeof(TEntidade).Name;
        protected readonly ITestOutputHelper _output;
        protected IBuilder<TEntidade> Builder { get; }
        protected IServicoCrud<TEntidade, ICrud<TEntidade>> Servico { get; }

        private IList<TEntidade> GerarItens(int qtdItens, params object[] args)
        {
            var itens = new List<TEntidade>();
            for (int i = 0; i < qtdItens; i++)
                itens.Add(Builder.DadosParaInsercao(args).Generate());
        
            return itens;
        }
        protected async Task<TEntidade[]> IncluirItensAsync(
            int qtdTestes, params object[] args)
        {
            Console.WriteLine(@"Inicializando a sequencia de inclusões");
            var itens = GerarItens(qtdTestes, args);
            var itensDb = await Servico.IncluirRange(itens);
            var ids = string.Join(
                ",",
                itensDb.Select(s => s.Id).ToArray());

            _output.WriteLine($"ID gerados para {NomeTipoGenerico} : {ids}");

            return await Task.FromResult(itens.ToArray());
        }

        private IList<TEntidade> AlterarItens(
            IList<TEntidade> itens)
        {
            for (int i = 0; i < itens.Count; i++)
                itens[i] = Builder.DadosParaAlteracao(itens[i]);

            return itens;
        }
        protected async Task<TEntidade[]> AlterarIntesAsync(
            IList<TEntidade> itens)
        {
            Console.WriteLine(@"Inicializando a sequencia de alterações");
            itens = AlterarItens(itens);
            var itensDb = await Servico.AlterarRange(itens);

            var ids = string.Join(
                ",",
                itensDb.Select(s => s.Id).ToArray());

            _output.WriteLine($"IDs de {NomeTipoGenerico} alterados : {ids}");

            return await Task.FromResult(itens.ToArray());
        }

        protected async Task<int> DeletarAsync(IList<TEntidade> itens)
        {
            Console.WriteLine(@"Inicializando a sequencia de remoções");
            var ids = itens.Select(s => s.Id).ToArray();
            var linhasAfetadas = await Servico.DeletarRange(ids);

            _output.WriteLine($"Quantidade de {NomeTipoGenerico} removidos : {linhasAfetadas}");

            return await Task.FromResult(linhasAfetadas);
        }
    }
}
