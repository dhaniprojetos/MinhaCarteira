﻿using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.WebApi.Controllers.Base;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public class CentroClassificacaoController : BaseController<CentroClassificacao>
    {
        public CentroClassificacaoController(IServicoCrud<CentroClassificacao> servico)
            : base(servico) { }
    }
}
