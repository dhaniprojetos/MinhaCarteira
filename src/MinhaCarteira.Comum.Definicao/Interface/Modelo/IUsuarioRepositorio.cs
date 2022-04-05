using System.Collections.Generic;
using System.Threading.Tasks;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo.Base;

namespace MinhaCarteira.Comum.Definicao.Interface.Modelo
{
    public interface IUsuarioRepositorio
        : ICrud<Usuario>
    {
        Task<bool> ArmazenarPreferenciaUsuario(int userId, IList<UsuarioPreferencia> preferencias);
    }
}
