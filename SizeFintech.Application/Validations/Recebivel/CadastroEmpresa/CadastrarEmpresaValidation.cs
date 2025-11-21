
using FluentValidation;
using SizeFintech.Application.ViewModels.Recebivel.CadastroEmpresa.Request;
using SizeFintech.Domain.Models.Enums;

namespace SizeFintech.Application.Validations.Recebivel.CadastroEmpresa
{
    public class CadastrarEmpresaValidation : AbstractValidator<RequestCadastrarEmpresaViewModel>
    {
        public CadastrarEmpresaValidation()
        {
            RuleFor(x => x.Nome)
           .NotEmpty().WithMessage("O nome é obrigatório.")
           .MaximumLength(150).WithMessage("O nome deve ter no máximo 150 caracteres.");

            RuleFor(x => x.Cnpj)
                .NotEmpty().WithMessage("O CNPJ é obrigatório.")
                .Must(ValidarCnpj).WithMessage("O CNPJ informado é inválido.");

            RuleFor(x => x.Faturamento)
                .GreaterThan(0).WithMessage("O faturamento mensal deve ser maior que zero.");

            RuleFor(x => x.Ramo)
                .IsInEnum().WithMessage("O ramo informado é inválido.")
                .Must(r => r == RamoEnum.Servicos || r == RamoEnum.Produtos)
                .WithMessage("O ramo deve ser 'Serviços' ou 'Produtos'.");
        }

        private bool ValidarCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());
            if (cnpj.Length != 14)
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }
}

