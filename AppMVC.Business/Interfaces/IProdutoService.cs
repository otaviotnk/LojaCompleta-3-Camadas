using System;
using System.Threading.Tasks;
using AppMVC.Business.Models;

namespace AppMVC.Business.Interfaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Remover(Guid id);
    }
}