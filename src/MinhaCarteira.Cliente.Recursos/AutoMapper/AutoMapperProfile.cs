using AutoMapper;
using MinhaCarteira.Cliente.Recursos.Models;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Modelo;

namespace MinhaCarteira.Cliente.Recursos.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Pessoa, PessoaViewModel>();
            CreateMap<PessoaViewModel, Pessoa>();

            CreateMap<InstituicaoFinanceira, InstituicaoFinanceiraViewModel>();
            //.ForMember(m => m.Icone, opt => opt.Ignore());
            CreateMap<InstituicaoFinanceiraViewModel, InstituicaoFinanceira>();
            //.ForMember(m => m.Icone, opt => opt.Ignore());

            CreateMap<ContaBancaria, ContaBancariaViewModel>();
            CreateMap<ContaBancariaViewModel, ContaBancaria>();

            CreateMap<Categoria, CategoriaViewModel>();
            CreateMap<CategoriaViewModel, Categoria>();

            CreateMap<CentroClassificacao, CentroClassificacaoViewModel>();
            CreateMap<CentroClassificacaoViewModel, CentroClassificacao>();

            CreateMap<MovimentoBancario, MovimentoBancarioViewModel>();
            CreateMap<MovimentoBancarioViewModel, MovimentoBancario>();

            CreateMap<AgendamentoItem, AgendamentoItemViewModel>();
            CreateMap<AgendamentoItemViewModel, AgendamentoItem>();

            CreateMap<Agendamento, AgendamentoViewModel>();
            CreateMap<AgendamentoViewModel, Agendamento>();
            
            CreateMap<UsuarioLoginViewModel, UsuarioLogin>();
            CreateMap<UsuarioToken, UsuarioTokenViewModel>();
        }
    }
}
