using AutoMapper;
using AppMVC.App.ViewModels;
using AppMVC.Business.Models;

//MAPEAMENTO VIA AUTOMAPPER ENTRE AS ENTIDADES E AS VIEW MODELS

namespace AppMVC.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
        }
    }
}