using AppMVC.Business.Intefaces;
using AppMVC.Business.Models;
using AppMVC.Business.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

//CLASSE BASE IMPLEMENTADA PELAS DEMAIS CLASSES SERVICES

namespace AppMVC.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        //CRIA UMA INSTANCIA DE NOTIFICACAO COM A MENSAGEM VINDA DO NOTIFICAR
        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if(validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}