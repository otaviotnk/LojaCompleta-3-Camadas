using FluentValidation;

//Validações feitas com o FluentValidation, para traduzir as mensagens de erro e personalizar

namespace AppMVC.Business.Models.Validations
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            string defaultMessage = "O campo {PropertyName} precisa ser fornecido";
            string lengthMessage = "O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres";

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage(defaultMessage)
                .Length(2, 200).WithMessage(lengthMessage);

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage(defaultMessage)
                .Length(2, 1000).WithMessage(lengthMessage);

            RuleFor(c => c.Valor)
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");
        }
    }
}