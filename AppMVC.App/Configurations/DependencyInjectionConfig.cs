using AppMVC.App.Extensions;
using AppMVC.Business.Intefaces;
using AppMVC.Business.Notificacoes;
using AppMVC.Business.Services;
using AppMVC.Data.Context;
using AppMVC.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

//INJEÇÃO DE DEPENDENCIAS PARA DIMINUIR O TAMANHO E ORGANIZAR MELHOR A STARTUP.CS

namespace AppMVC.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            //ADD A INTERFACE, PARA O REPOSITORIO
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

            //ADD A INTERFACE PARA O SERVICE
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            return services;
        }
    }
}