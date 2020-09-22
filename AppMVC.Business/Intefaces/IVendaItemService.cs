using AppMVC.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppMVC.Business.Intefaces
{
    public interface IVendaItemService : IDisposable
    {
        Task Adicionar(VendaItem vendaItem);
        Task Atualizar(VendaItem vendaItem);
        Task Remover(Guid id);
    }
}
