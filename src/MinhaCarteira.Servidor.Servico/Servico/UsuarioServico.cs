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
using System;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class UsuarioServico : ServicoBase<Usuario, IUsuarioRepositorio>, IUsuarioServico
    {
        private async Task<Usuario> ObterPorUsername(string username)
        {
            var criterio = new FiltroBase();

            var filtroUser = new FiltroOpcao(
                "username",
                TipoOperadorBusca.Igual,
                username);

            criterio.OpcoesFiltro.Add(filtroUser);
            var retorno = await Repositorio.Navegar(criterio);
            var usuario = retorno.Item2.SingleOrDefault();

            return usuario;
        }

        public UsuarioServico(IUsuarioRepositorio repositorio) : base(repositorio)
        {
        }

        public async Task<Resposta<UsuarioToken>> Login(UsuarioLogin userInfo)
        {
            var usuario = await ObterPorUsername(userInfo.Usuario);
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
                Roles = usuario.Papeis.Select(s => s.Papel.Nome).ToList(),
                Preferences = usuario.Preferencias.ToDictionary(x => x.Nome, x => x.Valor)
            };

            return new Resposta<UsuarioToken>(
                token,
                "Usuário localizado com sucesso.");
        }

        public async Task<Resposta<bool>> ArmazenarPreferenciaUsuario(string username, string chaveValor)
        {
            var usuario = await ObterPorUsername(username);
            var preferencias = chaveValor?
                .Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split('='))
                .ToDictionary(x => x[0], y => y[1]);

            foreach (var preferencia in preferencias)
            {
                var item = usuario.Preferencias
                    .SingleOrDefault(s => s.Nome == preferencia.Key);

                if (item != null)
                    item.Valor = preferencia.Value;
                else
                {
                    item = new UsuarioPreferencia()
                    {
                        UsuarioId = usuario.Id,
                        Nome = preferencia.Key,
                        Valor = preferencia.Value
                    };

                    usuario.Preferencias.Add(item);
                }
            }

            var alterado = await Repositorio.ArmazenarPreferenciaUsuario(
                usuario.Id, usuario.Preferencias);

            return await Task.FromResult(new Resposta<bool>(alterado));
        }

    }
}
