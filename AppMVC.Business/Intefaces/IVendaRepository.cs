using AppMVC.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMVC.Business.Intefaces
{
    public interface IVendaRepository : IRepository<Venda>
    {
        //Deve receber uma lista de Id de cliente na tabela Venda
        Task<IEnumerable<Venda>> ObterVendasCliente(Guid clienteId);
        Task<Venda> ObterCarrinhoCliente(Guid id);
    }
}
