using AppMVC.Business.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMVC.App.ViewModels
{
    public class VendaViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [DisplayFormat()]
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
        
        public int Quantidade { get; set; }

        [DisplayName("Cliente")]
        public Guid ClienteId { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public IEnumerable<ClienteViewModel> Clientes { get; set; }

        //Para tentar popular somente, dependendo dá p tirar essa responsabilidade do Pedido        
        public Guid ProdutoId { get; set; }
        public ProdutoViewModel Produto { get; set; }

        [NotMapped]
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
    }
}
