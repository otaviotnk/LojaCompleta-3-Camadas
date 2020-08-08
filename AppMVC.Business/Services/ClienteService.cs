using AppMVC.Business.Intefaces;
using AppMVC.Business.Models;
using AppMVC.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppMVC.Business.Services
{
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public ClienteService(IClienteRepository clienteRepository,
            IEnderecoRepository enderecoRepository,
            INotificador notificador) : base(notificador)
        {
            _clienteRepository = clienteRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Cliente cliente)
        {
            if (!ExecutarValidacao(new ClienteValidation(), cliente)
                || !ExecutarValidacao(new EnderecoValidation(), cliente.Endereco))
            {
                return;
            }

            if (_clienteRepository.Buscar(c => c.Documento == cliente.Documento).Result.Any())
            {
                Notificar("Já existe um cliente cadastrado com este documento");
                return;
            }

            await _clienteRepository.Adicionar(cliente);

        }

        public async Task Atualizar(Cliente cliente)
        {
            if (!ExecutarValidacao(new ClienteValidation(), cliente))
            {
                return;
            }
            if (_clienteRepository.Buscar(c => c.Documento == cliente.Documento && c.Id != cliente.Id).Result.Any())
            {
                Notificar("Já existe um cliente cadastrado com este documento");
                return;
            }
            await _clienteRepository.Atualizar(cliente);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco))
            {
                return;
            }
            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task Remover(Guid id)
        {
            var endereco = await _enderecoRepository.ObterEnderecoPorFornecedor(id);

            if (endereco != null)
            {
                await _enderecoRepository.Remover(endereco.Id);
            }

            await _clienteRepository.Remover(id);
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}
