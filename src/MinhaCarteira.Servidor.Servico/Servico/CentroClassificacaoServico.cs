using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Servidor.Controle.Servico.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.Controle.Servico
{
    public class CentroClassificacaoServico : ServicoBase<CentroClassificacao>
    {
        public CentroClassificacaoServico(ICrud<CentroClassificacao> repositorio) : base(repositorio)
        {
        }
    }
}
