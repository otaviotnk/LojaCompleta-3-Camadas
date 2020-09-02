using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMVC.Business.Models
{
    public class Venda : Entity
    {
        public decimal TotalVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public TipoVenda TipoVenda { get; set; }
        public StatusVenda StatusVenda { get; set; }
        public string Observacoes { get; set; }
        public int Quantidade { get; set; }

        //Relacionamento EF
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        //Relacionamento para tentar colocar vários produtos em um pedido
        public Guid ProdutoId { get; set; }
       
        [NotMapped]
        public IEnumerable<Produto> Produtos { get; set; }

        //Tentanto criar a bendita lista de produtos do pedido
        //public virtual ICollection<VendaItem> VendaItems { get; set; }


    }
}
