using AppMVC.Business.Intefaces;
using AppMVC.Business.Models;
using AppMVC.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppMVC.Business.Services
{
    public class VendaService : BaseService, IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public VendaService(IVendaRepository vendaRepository,
                                IEnderecoRepository enderecoRepository,
                                INotificador notificador) : base(notificador)
        {
            _vendaRepository = vendaRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Venda venda)
        {
            //Verifica se existe uma Venda cadastrada com o mesmo Id
            if (!ExecutarValidacao( new VendaValidation(), venda))
            {
                return;
            }
            //Ver se realmente é necessário
            if (_vendaRepository.Buscar(v => v.Id == venda.Id).Result.Any())
            {
                Notificar("Esta venda já foi cadastrada");
                return;
            }
            await _vendaRepository.Adicionar(venda);
        }

        public async Task Atualizar(Venda venda)
        {
            if (!ExecutarValidacao(new VendaValidation(), venda))
            {
                return;
            }
            if (_vendaRepository.Buscar(v => v.Id != venda.Id).Result.Any())
            {
                Notificar("Esta venda já foi cadastrada");
                return;
            }
            await _vendaRepository.Atualizar(venda);
        }       

        public async Task Remover(Guid id)
        {
            var status = new StatusVenda();

            if (status == StatusVenda.Cancelada)
            {
                await _vendaRepository.Remover(id);
            }
            
        }

        public void Dispose()
        {
            _vendaRepository?.Dispose();
        }
    }
}
