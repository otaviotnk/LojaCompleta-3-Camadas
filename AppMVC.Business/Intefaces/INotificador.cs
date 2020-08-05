using System.Collections.Generic;
using AppMVC.Business.Notificacoes;

namespace AppMVC.Business.Intefaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}