using System.Threading.Tasks;
using AppMVC.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;

//COMPONENTE SUMMARY, QUE É USADO PARA PREENCHER AS BAGS COM MENSAGENS DE ERRO OU SUCESSO

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