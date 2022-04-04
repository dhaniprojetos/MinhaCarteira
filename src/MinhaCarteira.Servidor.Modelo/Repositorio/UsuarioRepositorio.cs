using Microsoft.EntityFrameworkCore;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio.Base;
using System.Linq;

namespace MinhaCarteira.Servidor.Modelo.Repositorio
{
    public class UsuarioRepositorio
        : RepositorioBase<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(MinhaCarteiraContext contexto) : base(contexto)
        {
        }

        protected override IQueryable<Usuario> AdicionarIncludes(IQueryable<Usuario> source)
        {
            return source
                .Include(i => i.Papeis)
                    .ThenInclude(ti => ti.Papel);
        }
    }
}
