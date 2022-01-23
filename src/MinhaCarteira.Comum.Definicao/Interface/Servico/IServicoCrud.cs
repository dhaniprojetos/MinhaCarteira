using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaCarteira.Comum.Definicao.Interface.Servico
{
    public interface IServicoCrud<TEntidade> : ICrud<TEntidade>
    {
        ICrud<TEntidade> Repositorio { get; }
    }
}
