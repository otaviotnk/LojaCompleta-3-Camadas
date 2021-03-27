using AppMVC.Business.Models;
using System;
using System.Threading.Tasks;

namespace AppMVC.Business.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> ObterClienteEndereco(Guid id);
    }
}
