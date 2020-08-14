using AppMVC.Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppMVC.App.ViewModels
{
    public class VendaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public decimal TotalVenda { get; set; }

        [DisplayName("Data Venda")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataVenda { get; set; }

        [DisplayName("Tipo Pagamento")]
        public TipoVenda TipoVenda { get; set; }

        [DisplayName("Status Venda")]
        public StatusVenda StatusVenda { get; set; }

        [DisplayName("Observações")]
        [StringLength(500, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
        public string Observacoes { get; set; }

        [DisplayName("Cliente")]
        public Guid ClienteId { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public IEnumerable<ClienteViewModel> Clientes { get; set; }

        //Dados para preencher com o que vem de ClienteId
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }

    }
}
