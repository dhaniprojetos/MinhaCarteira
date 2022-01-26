using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Modelo.Repositorio
{
    public class MovimentoBancarioRepositorio : RepositorioBase<MovimentoBancario>
    {
        public MovimentoBancarioRepositorio(MinhaCarteiraContext contexto) : base(contexto)
        {
        }
    }
}
