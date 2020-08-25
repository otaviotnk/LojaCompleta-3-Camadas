using AppMVC.App.ViewModels;
using AppMVC.Business.Intefaces;
using AppMVC.Business.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using AppMVC.Data.Migrations;
using Microsoft.EntityFrameworkCore.Query.Internal;

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
        public async Task<IActionResult> Create()
        {
            var vendaViewModel = await PopularClientesProdutos(new VendaViewModel());
            return View(vendaViewModel);
        }

        // POST: Vendas/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendaViewModel vendaViewModel)
        {
            if (!ModelState.IsValid)
            {
                vendaViewModel = await PopularClientesProdutos(new VendaViewModel());

                return View(vendaViewModel);
            }

            var venda = _mapper.Map<Venda>(vendaViewModel);

            var produto = await _produtoRepository.ObterPorId(venda.ProdutoId);

            if (venda.Quantidade > produto.Quantidade)
            {
                ModelState.AddModelError("", "A quantidade do pedido excede o estoque do produto");
                vendaViewModel = await PopularClientesProdutos(new VendaViewModel());
                return View(vendaViewModel);
            }

            if (venda.Quantidade <= 0)
            {
                ModelState.AddModelError("", "A quantidade do produto deve ser maior que 0");
                vendaViewModel = await PopularClientesProdutos(new VendaViewModel());
                return View(vendaViewModel);
            }

            venda.StatusVenda = StatusVenda.Criada;
            venda.TotalVenda = venda.Quantidade * produto.Valor;

            produto.Quantidade += - venda.Quantidade;

            await _produtoRepository.Atualizar(produto);
            await _vendaService.Adicionar(venda);

            if (!OperacaoValida())
            {
                return View(vendaViewModel);
            }

            TempData["Sucesso"] = "Venda cadastrada com sucesso!";

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
            //ou vendarepository
            await _vendaService.Atualizar(venda);

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
            if (id == null)
            {
                return NotFound();
            }
            var vendaViewModel = await ObterVendaPorId(id);
            return View(vendaViewModel);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var venda = await ObterVendaPorId(id);
            var produto = await _produtoRepository.ObterPorId(venda.ProdutoId);
            var vendaViewModel = _mapper.Map<Venda>(venda);


            //Dá para mudar para um Switch/Case
            if (vendaViewModel.StatusVenda == StatusVenda.Criada)
            {
                await _vendaService.Remover(id);
                TempData["Sucesso"] = "Venda excluída com Sucesso!";

                produto.Quantidade += venda.Quantidade;
                await _produtoRepository.Atualizar(produto);

                return RedirectToAction(nameof(Index));

            }

            if (vendaViewModel.StatusVenda == StatusVenda.Cancelada)
            {
                TempData["Editado"] = "Esta venda já está Cancelada, porém não pode ser excluída pois há faturas!";
                return RedirectToAction(nameof(Delete));

            }

            vendaViewModel.StatusVenda = StatusVenda.Cancelada;
            await _vendaService.Atualizar(vendaViewModel);
            TempData["Erro"] = "Venda não foi excluída pois já foi faturada!";
            TempData["Erro"] = "Venda Cancelada! A Venda não foi excluída pois já foi faturada!";

            return RedirectToAction(nameof(Delete));



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

        private async Task<VendaViewModel> PopularClientesProdutos(VendaViewModel vendaViewModel)
        {
            vendaViewModel.Clientes = _mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepository.ObterTodos());
            vendaViewModel.Produtos = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos());
            return vendaViewModel;

        }

    }
}
