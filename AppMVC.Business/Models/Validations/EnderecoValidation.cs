using FluentValidation;

//Validações feitas com o FluentValidation, para traduzir as mensagens de erro e personalizar

namespace AppMVC.Business.Models.Validations
{
    public class EnderecoValidation : AbstractValidator<Endereco>
    {
        public EnderecoValidation()
        {
            string defaultMessage = "O campo {PropertyName} precisa ser fornecido";
            string lengthMessage = "O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres";

            RuleFor(c => c.Logradouro)
                .NotEmpty().WithMessage(defaultMessage)
                .Length(2, 200).WithMessage(lengthMessage);

            RuleFor(c => c.Bairro)
                .NotEmpty().WithMessage(defaultMessage)
                .Length(2, 100).WithMessage(lengthMessage);

            RuleFor(c => c.Cep)
                .NotEmpty().WithMessage(defaultMessage)
                .Length(8).WithMessage(lengthMessage);

            RuleFor(c => c.Cidade)
                .NotEmpty().WithMessage(defaultMessage)
                .Length(2, 100).WithMessage(lengthMessage);

            RuleFor(c => c.Estado)
                .NotEmpty().WithMessage(defaultMessage)
                .Length(2, 50).WithMessage(lengthMessage);

            RuleFor(c => c.Numero)
                .NotEmpty().WithMessage(defaultMessage)
                .Length(1, 50).WithMessage(lengthMessage);
        }
    }
}