
using FluentValidation;
using SizeFintech.Application.ViewModels.Company.InsertCompany.Request;
using SizeFintech.Application.ViewModels.Invoice.InsertInvoice.Request;

namespace SizeFintech.Application.Validations.Invoice.InsertInvoice
{
    public class InsertInvoiceValidation : AbstractValidator<RequestInsertInvoiceViewModel>
    {
        public InsertInvoiceValidation()
        {
            RuleFor(x => x.Numero)
                .GreaterThan(0)
                .WithMessage("O número da nota fiscal é obrigatório e deve ser maior que zero.");

            RuleFor(x => x.ValorBruto)
                .GreaterThan(0)
                .WithMessage("O valor bruto da nota fiscal é obrigatório e deve ser maior que zero.");

            RuleFor(x => x.DataVencimento)
                .NotEmpty().WithMessage("A data de vencimento é obrigatória.")
                .Must(BeAValidDate).WithMessage("A data de vencimento deve ser uma data válida.")
                .Must(BeGreaterThanToday).WithMessage("A data de vencimento deve ser maior que a data atual.");

            RuleFor(x => x.EmpresaId)
                .GreaterThan(0)
                .WithMessage("O ID da empresa deve ser maior que zero.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date != default;
        }

        private bool BeGreaterThanToday(DateTime date)
        {
            return date.Date > DateTime.Now.Date;
        }
    }
}

