using Microsoft.EntityFrameworkCore;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio.Base;
using System.Linq;

namespace MinhaCarteira.Servidor.Modelo.Repositorio
{
    public class AgendamentoRepositorio : RepositorioBase<Agendamento>
    {
        public AgendamentoRepositorio(MinhaCarteiraContext contexto) : base(contexto)
        {
        }

        protected override IQueryable<Agendamento> AdicionarIncludes(IQueryable<Agendamento> source)
        {
            return source
                .Include(i => i.Categoria)
                    .ThenInclude(ti => ti.CategoriaPai)
                    .ThenInclude(ti => ti.CategoriaPai)
                .Include(i => i.CentroClassificacao)
                .Include(i => i.Pessoa)
                .Include(i => i.ContaBancaria)
                .Include(i => i.Items)
                    .ThenInclude(ti => ti.Movimentos)
                .Include(i => i.Items)
                    .ThenInclude(ti => ti.Pessoa)
                .Include(i => i.Items)
                    .ThenInclude(ti => ti.ContaBancaria);
        }
    }
}
