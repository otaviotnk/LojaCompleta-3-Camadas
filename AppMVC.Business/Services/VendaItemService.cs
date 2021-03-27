using AppMVC.Business.Interfaces;
using AppMVC.Business.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppMVC.Business.Services
{
    public class VendaItemService : BaseService, IVendaItemService
    {
        private readonly IVendaItemRepository _vendaItemRepository;

        public VendaItemService(IVendaItemRepository vendaItemRepository,
                                INotificador notificador) : base(notificador)
        {
            _vendaItemRepository = vendaItemRepository;
        }

        public async Task Adicionar(VendaItem vendaItem)
        {            
            if (_vendaItemRepository.Buscar(v => v.Id == vendaItem.Id).Result.Any())
            {
                Notificar("Este Item já foi adicionado");
                return;
            }

            await _vendaItemRepository.Adicionar(vendaItem);
        }

        public async Task Atualizar(VendaItem vendaItem)
        {
            await _vendaItemRepository.Atualizar(vendaItem);
        }        

        public async Task Remover(Guid id)
        {
            await _vendaItemRepository.Remover(id);
        }

        public void Dispose()
        {
            _vendaItemRepository?.Dispose();
        }
    }
}
