using AppMVC.Business.Models;
using System;
using System.Threading.Tasks;

namespace AppMVC.Business.Intefaces
{
    public interface IClienteService : IDisposable
    {
        Task Adicionar(Cliente cliente);
        Task Atualizar(Cliente cliente);
        Task Remover(Guid id);
        Task AtualizarEndereco(Endereco endereco);

    }
}
