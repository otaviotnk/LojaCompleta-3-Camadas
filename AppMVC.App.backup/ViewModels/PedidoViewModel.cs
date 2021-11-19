using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppMVC.App.ViewModels
{
    public class PedidoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Valor Total")]
        public decimal ValorTotalProduto { get; set; }

        [DisplayName("Quantidade Total")]
        public int QuantidadeTotalProduto { get; set; }

        
        [DisplayName("Produto")]
        public Guid ProdutoId { get; set; }
        public ProdutoViewModel Produto { get; set; }
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }

        [DisplayName("Venda")]
        public Guid VendaId { get; set; }
        public VendaViewModel Venda { get; set; }
    }
}
