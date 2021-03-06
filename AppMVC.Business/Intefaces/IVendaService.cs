﻿using AppMVC.Business.Models;
using System;
using System.Threading.Tasks;

namespace AppMVC.Business.Intefaces
{
    public interface IVendaService : IDisposable
    {
        Task Adicionar(Venda venda);
        Task Atualizar(Venda venda);
        Task Remover(Guid id);        
    }
}
