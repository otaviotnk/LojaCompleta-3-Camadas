using System;
using System.Collections.Generic;
using System.Text;

namespace AppMVC.Business.Models
{
    public class VendaItem
    {
        public Guid VendaId { get; set; }
        public virtual Venda Venda { get; set; }
        public Guid ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
