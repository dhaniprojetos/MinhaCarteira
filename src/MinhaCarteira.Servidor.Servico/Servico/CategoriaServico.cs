﻿using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Controle.Servico.Base;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class CategoriaServico : ServicoBase<Categoria>
    {
        public CategoriaServico(ICrud<Categoria> repositorio) : base(repositorio)
        {
        }
    }
}