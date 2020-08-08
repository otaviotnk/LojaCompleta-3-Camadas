namespace AppMVC.Business.Notificacoes
{
    //A notificação recebe uma mensagem aqui
    public class Notificacao
    {
        public Notificacao(string mensagem)
        {
            Mensagem = mensagem;
        }
        public string Mensagem { get; }
    }
}