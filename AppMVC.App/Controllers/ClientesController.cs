using AppMVC.App.Extensions;
using AppMVC.App.ViewModels;
using AppMVC.Business.Intefaces;
using AppMVC.Business.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMVC.App.Controllers
{
    [ClaimsAuthorize("Cliente", "")]
    public class ClientesController : BaseController
    {
        //Injetando os Repositorios, Servicoes e Mapeamento
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClientesController(IClienteRepository clienteRepository,
                                    IClienteService clienteService,
                                    IMapper mapper,
                                    INotificador notificador) : base(notificador)
        {
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;
            _mapper = mapper;
        }

        // GET: Clientes
        [Route("lista-de-clientes")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepository.ObterTodos()));
        }

        // GET: Clientes/Details/5
        [Route("dados-do-cliente/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var clienteViewModel = await ObterClienteEndereco(id);
            if (clienteViewModel == null)
            {
                return NotFound();
            }

            return View(clienteViewModel);
        }

        // GET: Clientes/Create
        [Route("novo-cliente")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [ClaimsAuthorize("Cliente", "Adicionar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("novo-cliente")]
        public async Task<IActionResult> Create(ClienteViewModel clienteViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(clienteViewModel);
            }

            var cliente = _mapper.Map<Cliente>(clienteViewModel);

            cliente.DataCadastro = DateTime.Now;

            await _clienteRepository.Adicionar(cliente);

            if (!OperacaoValida())
            {
                return View(clienteViewModel);
            }

            TempData["Sucesso"] = "Cliente adicionado com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        // GET: Clientes/Edit/5
        [ClaimsAuthorize("Cliente", "Editar")]
        [Route("editar-cliente/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var clienteViewModel = await ObterClienteEndereco(id);

            if (clienteViewModel == null)
            {
                return NotFound();
            }

            return View(clienteViewModel);
        }

        // POST: Clientes/Edit/5 

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Cliente", "Editar")]
        [Route("editar-cliente/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, ClienteViewModel clienteViewModel)
        {
            if (id != clienteViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(clienteViewModel);

            }

            var cliente = _mapper.Map<Cliente>(clienteViewModel);

            await _clienteService.Atualizar(cliente);

            if (!OperacaoValida())
            {
                //Para retornar todas as informacoes de endereco novamente 
                return View(await ObterClienteEndereco(id));
            }

            TempData["Editado"] = "Cliente editado com sucesso!";
            return RedirectToAction(nameof(Index));
        }


        // GET: Clientes/Delete/5
        [ClaimsAuthorize("Cliente", "Excluir")]
        [Route("excluir-cliente/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var clienteViewModel = await ObterClienteEndereco(id);
                
            if (clienteViewModel == null)
            {
                return NotFound();
            }

            return View(clienteViewModel);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Cliente", "Excluir")]
        [Route("excluir-cliente/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cliente = await ObterClienteEndereco(id);

            if (cliente == null)
            {
                return NotFound();
            }

            await _clienteService.Remover(id);

            if (!OperacaoValida())
            {
                return View(cliente);
            }

            TempData["Sucesso"] = "Cliente removido com sucesso";

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [Route("obter-endereco-cliente/{id:guid}")]
        public async Task<IActionResult> ObterEndereco(Guid id)
        {
            var cliente = await ObterClienteEndereco(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return PartialView("_DetalhesEndereco", cliente);
        }

        [ClaimsAuthorize("Cliente","Editar")]
        [Route("atualizar-endereco-cliente/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            var cliente = await ObterClienteEndereco(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return PartialView("_AtualizarEndereco", new ClienteViewModel { Endereco = cliente.Endereco });
        }

        [ClaimsAuthorize("Cliente","Editar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("atualizar-endereco-cliente/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(ClienteViewModel clienteViewModel)
        {
            //Retira do ModelState o Nome e Documento para fazer a validacao somente do endereco
            ModelState.Remove("Nome");
            ModelState.Remove("Documento");
            ModelState.Remove("DataNascimento");
            //Ver quais outros atributos tem que remover

            if (ModelState.IsValid)
            {
                await _clienteService.AtualizarEndereco(_mapper.Map<Endereco>(clienteViewModel.Endereco));

                if (!OperacaoValida())
                {
                    return PartialView("_AtualizarEndereco", clienteViewModel);
                }

                var url = Url.Action("ObterEndereco", "Clientes", new
                {
                    id = clienteViewModel.Endereco.ClienteId
                });
                return Json(new { success = true, url });
            }

            return PartialView("_AtualizarEndereco", clienteViewModel);
        }

        private async Task<ClienteViewModel> ObterClienteEndereco(Guid id)
        {
            return _mapper.Map<ClienteViewModel>(await _clienteRepository.ObterClienteEndereco(id));
        }
    }
}
