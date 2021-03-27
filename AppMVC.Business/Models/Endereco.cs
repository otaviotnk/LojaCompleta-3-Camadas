using System;

namespace AppMVC.Business.Models
{
    public class Endereco : Entity
    {       
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public Guid? FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public Guid? ClienteId { get; set; }
        public Cliente Cliente { get; set; }

    }
}