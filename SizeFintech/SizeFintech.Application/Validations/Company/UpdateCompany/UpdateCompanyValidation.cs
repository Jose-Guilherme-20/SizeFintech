using FluentValidation;
using SizeFintech.API.Extensions;
using SizeFintech.Application.ViewModels.Company.UpdateCompany.Request;
using SizeFintech.Domain.Models.Enums;

namespace SizeFintech.Application.Validations.Company.UpdateCompany
{
    public class UpdateCompanyValidation : AbstractValidator<RequestUpdateCompanyViewModel>
    {
        public UpdateCompanyValidation()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("O Id da empresa deve ser maior que zero.");

            RuleFor(x => x.Nome)
           .NotEmpty().WithMessage("O nome é obrigatório.")
           .MaximumLength(150).WithMessage("O nome deve ter no máximo 150 caracteres.");

            RuleFor(x => x.Cnpj)
                .NotEmpty().WithMessage("O CNPJ é obrigatório.")
                .Must(cnpj => cnpj.ValidarCnpj()).WithMessage("O CNPJ informado é inválido.");

            RuleFor(x => x.Faturamento)
                .GreaterThan(0).WithMessage("O faturamento mensal deve ser maior que zero.");

            RuleFor(x => x.Ramo)
                .IsInEnum().WithMessage("O ramo informado é inválido.")
                .Must(r => r == RamoEnum.Servicos || r == RamoEnum.Produtos)
                .WithMessage("O ramo deve ser 'Serviços' ou 'Produtos'.");
        }
    }
}
