using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaCarteira.Comum.Definicao.Interface.Servico
{
    public interface IInstituicaoFinanceiraServico
        : IServicoCrud<InstituicaoFinanceira, ICrud<InstituicaoFinanceira>>
    {
    }
}
