
using SizeFintech.Domain.Models.Enums;

namespace SizeFintech.Application.ViewModels.Recebivel.CadastroEmpresa.Request
{
    public class RequestCadastrarEmpresaViewModel
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public decimal Faturamento { get; set; }
        public RamoEnum Ramo { get; set; }
    }
}
