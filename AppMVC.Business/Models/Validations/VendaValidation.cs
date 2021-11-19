using FluentValidation;

namespace AppMVC.Business.Models.Validations
{
    public class VendaValidation : AbstractValidator<Venda>
    {
        public VendaValidation()
        {
            string lengthMessage = "O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres";

            RuleFor(v => v.Observacoes)
                .Length(5, 500)
                .WithMessage(lengthMessage);

        }
    }
}
