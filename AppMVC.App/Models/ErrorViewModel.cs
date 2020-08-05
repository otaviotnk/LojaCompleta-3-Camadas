using System;

//PERSONALIZADO PARA APARECER OS ERROS CONFORME OS ERROS NA HOME CONTROLLER E NA STARTUP.CS
//PERSONALIZADO TAMBÉM A VIEW ERROR 

namespace AppMVC.App.Models
{
    public class ErrorViewModel
    {
        public int ErroCode { get; set; }

        public string Titulo { get; set; }

        public string Mensagem { get; set; }

    }
}
