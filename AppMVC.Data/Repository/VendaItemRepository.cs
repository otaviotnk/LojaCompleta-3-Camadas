using AppMVC.Business.Intefaces;
using AppMVC.Business.Models;
using AppMVC.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppMVC.Data.Repository
{
    public class VendaItemRepository : Repository<VendaItem>, IVendaItemRepository
    {
        public VendaItemRepository(MeuDbContext context) : base(context) { }
        
        public async Task<IEnumerable<VendaItem>> ObterProdutosVenda(Guid vendaId)
        {
            return await Buscar(vi => vi.VendaId == vendaId);
        }
    }
}
