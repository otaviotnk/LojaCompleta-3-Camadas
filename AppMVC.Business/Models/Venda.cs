using System;

namespace AppMVC.Business.Models
{
    public class Venda : Entity
    {
        public decimal TotalVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public TipoVenda TipoVenda { get; set; }
        public StatusVenda StatusVenda { get; set; }
        public string Observacoes { get; set; }

        //Para salvar o endereço de entrega do pedido
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }


        //Relacionamento EF
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        //AQUI DEVE ENTRAR O RELACIONAMENTO COM  PEDIDO
        
    }
}
