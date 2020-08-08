using AppMVC.Business.Models;
using System;
using System.Threading.Tasks;

namespace AppMVC.Business.Intefaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> ObterClienteEndereco(Guid id);
    }
}
