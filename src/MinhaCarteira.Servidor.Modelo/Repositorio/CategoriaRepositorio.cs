using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio.Base;

namespace MinhaCarteira.Servidor.Modelo.Repositorio
{
    public class CategoriaRepositorio : RepositorioBase<Categoria>
    {
        public CategoriaRepositorio(MinhaCarteiraContext contexto) : base(contexto)
        {
        }

        protected override IQueryable<Categoria> AdicionarIncludes(IQueryable<Categoria> source)
        {
            return source
                .Include(i => i.CategoriaPai)
                    .ThenInclude(ti => ti.CategoriaPai)
                    .ThenInclude(ti => ti.CategoriaPai)
                .Include(i => i.SubCategoria)
                    .ThenInclude(ti => ti.SubCategoria)
                    .ThenInclude(ti => ti.SubCategoria);
        }

        //protected override IQueryable<Categoria> AdicionarOrdenacao(IQueryable<Categoria> source)
        //{
        //    return source.OrderBy(o => o.Caminho);
        //}

        //private void PrepararPersistencia(Categoria item)
        //{
        //    var ids = item.SubCategoria.Select(s => s.Id).ToArray();

        //    var itemDb = ObterPorId(item.Id).Result;
        //    var removidos = itemDb.SubCategoria
        //        .Where(w => !ids.Contains(w.Id))
        //        .ToList();

        //    Contexto.RemoveRange(removidos);
        //    foreach (var categoria in removidos)
        //        Contexto.Entry(categoria).State = EntityState.Detached;
        //}

        //protected override async Task<IList<Categoria>> ExecutarAntesAlterar(IList<Categoria> itens)
        //{
        //    itens.ToList().ForEach(async f =>
        //    {
        //        var ids = f.SubCategoria.Select(s => s.Id).ToArray();
        //
        //        var itemDb = await ObterPorId(f.Id);
        //        var removidos = itemDb.SubCategoria
        //            .Where(w => !ids.Contains(w.Id))
        //            .ToList();
        //
        //        Contexto.RemoveRange(removidos);
        //        foreach (var categoria in removidos)
        //            Contexto.Entry(categoria).State = EntityState.Detached;
        //    });
        //
        //    return await Task.FromResult(itens);
        //    //return await base.ExecutarAntesAlterar(itens);
        //}

        protected override async Task<IList<Categoria>> ExecutarAntesAlterar(IList<Categoria> itens)
        {

            var ids = itens
                .Select(s => s.Id)
                .ToArray();
            var subIds = itens
                .SelectMany(s => s.SubCategoria, (_, i) => i.Id)
                .Select(s => s)
                .Where(w => w > 0)
                .ToArray();

            var itensDb = await AdicionarIncludes(Contexto.Categorias)
                .AsNoTracking()
                .Where(w => ids.Contains(w.Id))
                .ToListAsync();

            var subIdsDb = itensDb
                .SelectMany(s => s.SubCategoria, (_, i) => i.Id)
                .Select(s => s)
                .ToArray();

            var subCategoriasRemovidas = subIdsDb
                .Where(w => !subIds.Contains(w))
                .ToArray();

            await DeletarRange(subCategoriasRemovidas);

            return itens;
        }
    }
}
