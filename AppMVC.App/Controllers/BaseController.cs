using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppMVC.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;

//Controler que serve como base para as demais controllers do projeto

namespace AppMVC.App.Controllers
{
    public abstract class BaseController : Controller
    {
        //Injecao de dependencia do Notificador
        private readonly INotificador _notificador;

        protected BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        //retorna True caso não tenha nenhuma notificacao
        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }
    }
}
