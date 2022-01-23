using Microsoft.AspNetCore.Mvc;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaCarteira.Servidor.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IServicoCrud<Pessoa> _servico;

        public PessoaController(IServicoCrud<Pessoa> servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public async Task<IList<Pessoa>> Get()
        {
            return await _servico.Navegar(null);
        }
    }
}
