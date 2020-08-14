using AppMVC.Business.Intefaces;
using AppMVC.Business.Models;
using AppMVC.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppMVC.Data.Repository
{
    public class VendaRepository : Repository<Venda>, IVendaRepository
    {
        public VendaRepository(MeuDbContext context) : base(context) { }

        public async Task<IEnumerable<Venda>> ObterVendasCliente(Guid clienteId)
        {
            return await Buscar(v => v.ClienteId == clienteId);

        }

        public async Task<Venda> ObterCarrinhoCliente(Guid id)
        {
            return await Db.Vendas.AsNoTracking()
                .Include(c => c.Cliente)
                //.Include(p => p.Produtos)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
    
}
