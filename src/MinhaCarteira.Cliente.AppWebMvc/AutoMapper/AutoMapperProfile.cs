using AutoMapper;
using MinhaCarteira.Cliente.AppWebMvc.Models;
using MinhaCarteira.Cliente.AppWebMvc.ViewModel;
using MinhaCarteira.Comum.Definicao.Entidade;

namespace MinhaCarteira.Cliente.AppWebMvc.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Pessoa, PessoaViewModel>();
            CreateMap<PessoaViewModel, Pessoa>();
            
            CreateMap<InstituicaoFinanceira, InstituicaoFinanceiraViewModel>();
            CreateMap<InstituicaoFinanceiraViewModel, InstituicaoFinanceira>();

            CreateMap<ContaBancaria, ContaBancariaViewModel>();
            CreateMap<ContaBancariaViewModel, ContaBancaria>();

            CreateMap<Categoria, CategoriaViewModel>();
            CreateMap<CategoriaViewModel, Categoria>();

            CreateMap<CentroClassificacao, CentroClassificacaoViewModel>();
            CreateMap<CentroClassificacaoViewModel, CentroClassificacao>();

            CreateMap<MovimentoBancario, MovimentoBancarioViewModel>();
            CreateMap<MovimentoBancarioViewModel, MovimentoBancario>();
        }
    }
}
