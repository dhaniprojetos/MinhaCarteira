using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Filtro;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Comum.Definicao.Modelo;
using MinhaCarteira.Comum.Definicao.Modelo.Servico;
using MinhaCarteira.Servidor.Controle.Servico.Base;
using System.Threading.Tasks;
using System.Linq;
using MinhaCarteira.Servidor.Modelo.Helper;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class UsuarioServico : ServicoBase<Usuario, IUsuarioRepositorio>, IUsuarioServico
    {
        public UsuarioServico(IUsuarioRepositorio repositorio) : base(repositorio)
        {
        }

        public async Task<Resposta<UsuarioToken>> Login(UsuarioLogin userInfo)
        {
            var criterio = new FiltroBase();

            var filtroUser = new FiltroOpcao(
                "username",
                TipoOperadorBusca.Igual,
                userInfo.Usuario);

            criterio.OpcoesFiltro.Add(filtroUser);
            var retorno = await Repositorio.Navegar(criterio);
            var usuario = retorno.Item2.SingleOrDefault();

            if (usuario == null)
            {
                return new Resposta<UsuarioToken>(null, "Usuário não localizado")
                {
                    BemSucedido = false,
                    StatusCode = 401
                };
            }

            if (!usuario.PasswordHash.VerificarHashSenha(userInfo.Senha))
            {
                return new Resposta<UsuarioToken>(null, "Usuário/Senha inválidos.")
                {
                    BemSucedido = false,
                    StatusCode = 401
                };
            }

            var token = new UsuarioToken(usuario)
            {
                Roles = usuario.Papeis.Select(s => s.Papel.Nome).ToList()
            };

            return new Resposta<UsuarioToken>(
                token,
                "Usuário localizado com sucesso.");
        }
    }
}
