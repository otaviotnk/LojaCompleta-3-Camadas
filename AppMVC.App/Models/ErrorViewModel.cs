//Personalizado para aparecer os erros conforme os erros definidos na HomeController e na Startup.sc
//Funciona somente em produção
//Personalizada também a View Error

namespace AppMVC.App.Models
{
    public class ErrorViewModel
    {
        public int ErroCode { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
    }
}
