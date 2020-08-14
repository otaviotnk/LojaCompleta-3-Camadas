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
    public class FornecedoresController : BaseController
    {
        //Injetando os Repositorios, Servicos e Mapeamento
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository,
                                        IFornecedorService fornecedorService,
                                        IMapper mapper,
                                        INotificador notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        // GET: Fornecedores
        [ClaimsAuthorize("Fornecedor","")]

        [Route("lista-de-fornecedores")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos()));
        }

        // GET: Fornecedores/Details/5
        [AllowAnonymous]
        [Route("dados-do-fornecedor/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        // GET: Fornecedores/Create
        //ClaimsAuthorize vem da classe de extensão
        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        [Route("novo-fornecedor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fornecedores/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        [Route("novo-fornecedor")]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(fornecedorViewModel);
            }

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);

            //Seta a data de cadastro do fornecedor como a data atual
            fornecedor.DataCadastro = DateTime.Now;           
            
            await _fornecedorService.Adicionar(fornecedor);

            if (!OperacaoValida())
            {
                return View(fornecedorViewModel);
            }

            TempData["Sucesso"] = "Fornecedor adicionado com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        // GET: Fornecedores/Edit/5
        [ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }
            
            return View(fornecedorViewModel);
        }

        // POST: Fornecedores/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id,FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(fornecedorViewModel);
            }

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);

            await _fornecedorService.Atualizar(fornecedor);

            if (!OperacaoValida())
            {
                //Para retornar todas as informacoes novamente 
                return View(await ObterFornecedorProdutosEndereco(id));
            }

            TempData["Editado"] = "Fornecedor editado com sucesso!";

            return RedirectToAction(nameof(Index));            
        }

        // GET: Fornecedores/Delete/5
        [ClaimsAuthorize("Fornecedor", "Excluir")]
        [Route("excluir-fornecedor/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        // POST: Fornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Fornecedor", "Excluir")]
        [Route("excluir-fornecedor/{id:guid}")]     
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            await _fornecedorService.Remover(id);

            if (!OperacaoValida())
            {
                return View(fornecedor);
            }

            TempData["Sucesso"] = "Fornecedor removido com sucesso";

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [Route("obter-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> ObterEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null)
            {
                return NotFound();
            }
            
            return PartialView("_DetalhesEndereco", fornecedor);
        }

        [ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return PartialView("_AtualizarEndereco", new FornecedorViewModel { Endereco = fornecedor.Endereco });
        }

        [ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> AtualizarEndereco(FornecedorViewModel fornecedorViewModel)
        {
            //Retira do ModelState o Nome e Documento para fazer a validacao somente do endereco
            ModelState.Remove("Nome");
            ModelState.Remove("Documento");

            if (ModelState.IsValid)
            {
                await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(fornecedorViewModel.Endereco));

                if (!OperacaoValida())
                {
                    return PartialView("_AtualizarEndereco", fornecedorViewModel);
                }

                var url = Url.Action("ObterEndereco", "Fornecedores", new
                {
                    id = fornecedorViewModel.Endereco.FornecedorId
                });
                return Json(new { success = true, url });
            }

            return PartialView("_AtualizarEndereco", fornecedorViewModel);
        }

        private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }
    }
}
