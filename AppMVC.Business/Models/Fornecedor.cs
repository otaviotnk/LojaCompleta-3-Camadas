using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AppMVC.Business.Enums;

namespace AppMVC.Business.Models
{
    public class Fornecedor : Entity
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public DateTime DataCadastro { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
        public Endereco Endereco { get; set; }
        public bool Ativo { get; set; }

        [NotMapped]
        public IEnumerable<Produto> Produtos { get; set; }
    }
}