
using SizeFintech.Domain.Models.Entities;
using SizeFintech.Domain.Models.Enums;

namespace SizeFintech.Application.ViewModels.Recebivel.CadastroEmpresa.Request
{
    public class RequestCadastrarEmpresaViewModel
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public decimal Faturamento { get; set; }
        public RamoEnum Ramo { get; set; }

        public EmpresaEntity ToEntity()
        {
            return new EmpresaEntity
            {
                Nome = Nome,
                Cnpj = Cnpj,
                Faturamento = Faturamento,
                RamoId = (int)Ramo
            };
        }
    }
}
