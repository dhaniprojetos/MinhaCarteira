using System;
using System.Linq;
using Bogus;
using Bogus.DataSets;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Teste;

namespace MinhaCarteira.Teste.Mock.Faker
{
    public class CategoriaBuilder : IBuilder<Categoria>
    {
        // ReSharper disable once UnusedParameter.Local
        private static Categoria ItemFake(int id)
        {
            var cmc = new Commerce();
            var faker = new Faker<Categoria>("pt_BR")
                .StrictMode(false)
                .RuleFor(p => p.Nome, f => cmc.Categories(1)[0])
                .RuleFor(p => p.IdAuxiliar, f => f.Random.Int(0, 500).OrNull(f, .8f));

            var retorno = faker.Generate();
            return retorno;
        }

        public Categoria DadosParaInsercao(params object[] args)
        {
            var categoria = ItemFake((int)args[0]);
            var random = new Random();
            var qtd = random.Next(1, 10);
            var subQtd = random.Next(1, 10);

            for (var i = 1; i <= qtd; i++)
            {
                categoria.SubCategoria.Add(ItemFake(i));
                
                for (int j = 0; j < subQtd; j++)
                    categoria.SubCategoria[i-1].SubCategoria.Add(ItemFake(j));
            }

            return categoria;
        }

        public Categoria DadosParaAlteracao(Categoria item)
        {
            item.Nome += " alterado";

            var categRemover = item.SubCategoria
                .Where(w => w.Id % 2 == 0)
                .ToList();

            foreach (var categoria in categRemover)
                item.SubCategoria.Remove(categoria);

            foreach (var subCategoria in item.SubCategoria)
                subCategoria.Nome += " sub alterado";

            for (var i = 0; i < categRemover.Count; i++)
            {
                var cat = ItemFake(i);
                cat.Nome += " sub adicionado";
                cat.CategoriaPai = item;
                cat.IdCategoriaPai = item.Id;
                item.SubCategoria.Add(cat);
            }

            return item;
        }
    }
}
