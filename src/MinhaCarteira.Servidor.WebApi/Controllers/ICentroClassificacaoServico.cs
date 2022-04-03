using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    public interface ICentroClassificacaoServico : IServicoCrud<CentroClassificacao, ICrud<CentroClassificacao>>
    {
    }
}
