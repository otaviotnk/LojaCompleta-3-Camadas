using FluentValidation;

namespace AppMVC.Business.Models.Validations
{
    public class VendaValidation : AbstractValidator<Venda>
    {
        public VendaValidation()
        {
            string defaultMessage = "O campo {PropertyName} precisa ser fornecido";
            string lengthMessage = "O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres";

            RuleFor(v => v.Observacoes)                
                .Length(5, 500)
                .WithMessage(lengthMessage);

            RuleFor(v => v.QuantidadeVenda)
                .NotEmpty().WithMessage(defaultMessage);            
            
        }
    }
}
