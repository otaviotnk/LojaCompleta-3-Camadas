using AppMVC.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMVC.Business.Interfaces
{
    public interface IVendaRepository : IRepository<Venda>
    {
        Task<IEnumerable<Venda>> ObterVendasCliente(Guid clienteId);

        Task<Venda> ObterCarrinhoCliente(Guid id);
    }
}
