using System;
using AppMVC.Business.Enums;

namespace AppMVC.Business.Models
{
    public class Cliente : Entity

    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public Genero Genero { get; set; }
        public DateTime DataNascimento { get; set; }
        public Endereco Endereco { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
