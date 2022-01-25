using Microsoft.AspNetCore.Mvc.Rendering;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaCarteira.Cliente.AppWebMvc.Models
{
    public class ContaBancariaViewModel : IEntidade
    {
        public ContaBancariaViewModel()
        {
            InstituicoesBancaria = new List<SelectListItem>();
        }
        public ContaBancariaViewModel(Resposta<IList<InstituicaoFinanceira>> reposta)
        {
            var items = reposta.Dados?
                .Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                })
                .ToList();

            InstituicoesBancaria = items;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }

        public int IdInstituicaoFinanceira { get; set; }
        public InstituicaoFinanceira InstituicaoFinanceira { get; set; }

        public IEnumerable<SelectListItem> InstituicoesBancaria { get; set; }
    }
}
