using AppMVC.Business.Models.Validations.Documentos;
using FluentValidation;

//Validações feitas com o FluentValidation, para traduzir as mensagens de erro e personalizar

namespace AppMVC.Business.Models.Validations
{
    class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            string defaultMessage = "O campo {PropertyName} precisa ser fornecido";
            string lengthMessage = "O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres";

            RuleFor(
                c => c.Nome)
                .NotEmpty().WithMessage(defaultMessage)
                .Length(3, 100)
                .WithMessage(lengthMessage);

            RuleFor(
                c => c.Genero)
                .NotEmpty().WithMessage(defaultMessage);

            RuleFor(
               c => c.DataNascimento)
               .NotEmpty().WithMessage(defaultMessage);

            RuleFor(c => c.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

            RuleFor(c => CpfValidacao.Validar(c.Documento)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");

        }
    }
}
