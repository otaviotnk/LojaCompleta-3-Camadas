using System.Collections.Generic;
using System.Linq;
using AppMVC.Business.Intefaces;

namespace AppMVC.Business.Notificacoes
{
    //O notificador implementa a Interface
    public class Notificador : INotificador
    {
        private readonly List<Notificacao> _notificacoes;

        public Notificador()
        {
            _notificacoes = new List<Notificacao>();
        }

        //Manipula a notificação, adicionando ela(s) na pilha        
        public void Handle(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }


        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }

        //VERIFICA SE EXISTE QUALQUER NOTIFICACAO
        public bool TemNotificacao()
        {
            return _notificacoes.Any();
        }
    }
}