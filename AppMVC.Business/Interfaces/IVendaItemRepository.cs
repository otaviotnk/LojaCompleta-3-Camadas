using AppMVC.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppMVC.Business.Interfaces
{
    public interface IVendaItemRepository : IRepository<VendaItem>
    {
        Task<IEnumerable<VendaItem>> ObterProdutosVenda(Guid vendaId);

    }
}
