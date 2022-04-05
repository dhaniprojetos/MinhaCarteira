using Microsoft.EntityFrameworkCore;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                .Include(i => i.Preferencias)
                .Include(i => i.Papeis)
                    .ThenInclude(ti => ti.Papel);
        }

        public async Task<bool> ArmazenarPreferenciaUsuario(int userId, IList<UsuarioPreferencia> preferencias)
        {
            try
            {
                var preferenciasDb = Contexto.Preferencias
                    .Where(w => w.UsuarioId == userId)
                    .ToList();

                var novos = new List<UsuarioPreferencia>();
                var removidos = new List<UsuarioPreferencia>();
                var atualizados = new List<UsuarioPreferencia>();

                foreach (var preferencia in preferenciasDb)
                {
                    var item = preferencias.FirstOrDefault(w => w.Nome == preferencia.Nome);
                    if (item == null)
                        removidos.Add(preferencia);
                }
                
                foreach (var preferencia in preferencias)
                {
                    var item = preferenciasDb.FirstOrDefault(w => w.Nome == preferencia.Nome);
                    if (item == null)
                        novos.Add(preferencia);
                }

                foreach (var preferencia in preferencias)
                {
                    var item = preferenciasDb.FirstOrDefault(w => w.Nome == preferencia.Nome);
                    if (item != null)
                    {
                        item.Valor = preferencia.Valor;
                        atualizados.Add(item);
                    }
                }

                Contexto.Preferencias.RemoveRange(removidos);
                Contexto.Preferencias.UpdateRange(atualizados);
                await Contexto.Preferencias.AddRangeAsync(novos);

                var linhasAfetadas = await Contexto.SaveChangesAsync();

                return linhasAfetadas > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
