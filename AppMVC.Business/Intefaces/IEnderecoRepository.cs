using System;
using System.Threading.Tasks;
using AppMVC.Business.Models;

namespace AppMVC.Business.Intefaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
        Task<Endereco> ObterEnderecoPorCliente(Guid clienteId);
    }
}