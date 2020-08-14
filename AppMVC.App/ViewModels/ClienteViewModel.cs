using AppMVC.Business.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppMVC.App.ViewModels
{
    public class ClienteViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(11, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 11)]
        public string Documento { get; set; }

        [DisplayName("Gênero")]
        public Genero Genero { get; set; }

        [DisplayName("Tipo")]
        public int TipoPessoa { get; set; }

        [DisplayName("Data de Nascimento")]
        [Required(ErrorMessage = "Favor informar uma Data")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime DataNascimento { get; set; }

        public EnderecoViewModel Endereco { get; set; }

        public DateTime DataCadastro { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }

    }
}
