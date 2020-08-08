using System.Threading.Tasks;
using AppMVC.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;

//Componente de View, que é usado para preencher as Bags de notificação com mensagens de erro, sucesso, alertas, etc...

namespace AppMVC.App.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotificador _notificador;

        public SummaryViewComponent(INotificador notificador)
        {
            _notificador = notificador;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notificador.ObterNotificacoes());
            notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Mensagem));

            return View();
        }
    }
}