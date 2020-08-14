using AppMVC.App.ViewModels;
using AppMVC.Business.Intefaces;
using AppMVC.Business.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMVC.App.Controllers
{
    public class VendasController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IVendaRepository _vendaRepository;
        private readonly IVendaService _vendaService;       
        private readonly IMapper _mapper;

        public VendasController(IVendaRepository vendaRepository,
                                    IVendaService vendaService,
                                    IProdutoRepository produtoRepository,
                                    IClienteRepository clienteRepository,
                                    IMapper mapper,
                                    INotificador notificador) : base(notificador)
        {
            _vendaRepository = vendaRepository;
            _vendaService = vendaService;
            _produtoRepository = produtoRepository;
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }


        // GET: Vendas
        public async Task<IActionResult> Index()
        {            
            return View(_mapper.Map<IEnumerable<VendaViewModel>>(await _vendaRepository.ObterTodos()));
        }

        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var vendaViewModel = await ObterVendaPorId(id);
            if (vendaViewModel == null)
            {
                return NotFound();
            }

            return View(vendaViewModel);
        }

        // GET: Vendas/Create
        public async Task<IActionResult> CreateAsync()
        {
            var vendaViewModel = await PopularClientes(new VendaViewModel());
            return View(vendaViewModel);
        }

        // POST: Vendas/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendaViewModel vendaViewModel)
        {
            vendaViewModel = await PopularClientes(new VendaViewModel());

            if (!ModelState.IsValid)
            {
                return View(vendaViewModel);
            }
            var venda = _mapper.Map<Venda>(vendaViewModel);

            venda.DataVenda = DateTime.Now;
            venda.StatusVenda = StatusVenda.Criada;

            await _vendaRepository.Adicionar(venda);

            if (!OperacaoValida())
            {
                return View(vendaViewModel);

            }

            TempData["Sucesso"] = "Venda Cadastrada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Vendas/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var vendaViewModel = await ObterVendaPorId(id);

            if (vendaViewModel == null)
            {
                return NotFound();
            }
            return View(vendaViewModel);
        }

        // POST: Vendas/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, VendaViewModel vendaViewModel)
        {
            if (id != vendaViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(vendaViewModel);
            }

            var venda = _mapper.Map<Venda>(vendaViewModel);
            await _vendaRepository.Atualizar(venda);

            if (!OperacaoValida())
            {
                return View(await ObterVendaPorId(id));
            }

            TempData["Editado"] = "Venda editada com sucesso!";

            return RedirectToAction(nameof(Index));

        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var vendaViewModel = await ObterVendaPorId(id);

            if (vendaViewModel == null)
            {
                return NotFound();
            }
            return View(vendaViewModel);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var venda = await ObterVendaPorId(id);
            if (venda == null)
            {
                return NotFound();
            }

            venda.StatusVenda = StatusVenda.Cancelada;

            //criar metodo para atualizar status
            //await _vendaService.Atualizar(venda);
            await _vendaService.Remover(id);

            if (!OperacaoValida())
            {
                return View(venda);
            }
            TempData["Sucesso"] = "Venda removida com sucesso";

            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> AdicionarProduto()
        {
            var vendaViewModel = await PopularClientes(new VendaViewModel());
            return PartialView("_AdicionarProduto", vendaViewModel);
            
        }

        private async Task<VendaViewModel> ObterVendaPorId(Guid id)
        {
            return _mapper.Map<VendaViewModel>(await _vendaRepository.ObterPorId(id));
        }

        private async Task<VendaViewModel> PopularClientes(VendaViewModel vendaViewModel)
        {
            vendaViewModel.Clientes = _mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepository.ObterTodos());
            return vendaViewModel;
        }

        //COLOCAR NA CONTROLLER DE PEDIDOS

        //private async Task<VendaViewModel> PopularProdutos(VendaViewModel vendaViewModel)
        //{
        //    vendaViewModel.Produtos = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos());
        //    return vendaViewModel;
        //}               

        //private async Task<VendaViewModel> PopularClientesProdutos(VendaViewModel vendaViewModel) 
        //{
        //    vendaViewModel.Clientes = _mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepository.ObterTodos());
        //    vendaViewModel.Produtos = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos());
        //    return vendaViewModel;

        //}

    }
}
