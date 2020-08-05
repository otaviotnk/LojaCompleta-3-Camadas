namespace AppMVC.Business.Notificacoes
{
    //A NOTIFICACAO RECEBE UMA MENSAGEM
    public class Notificacao
    {
        public Notificacao(string mensagem)
        {
            Mensagem = mensagem;
        }
        public string Mensagem { get; }
    }
}