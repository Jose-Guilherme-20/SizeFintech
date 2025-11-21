
using SizeFintech.Domain.Models.Entities;
using SizeFintech.Domain.Models.Enums;

namespace SizeFintech.Application.ViewModels.Company.UpdateCompany.Request
{
    public class RequestUpdateCompanyViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public decimal Faturamento { get; set; }
        public RamoEnum Ramo { get; set; }

        public CompanyEntity Update(CompanyEntity entity)
        {
            entity.Nome = Nome;
            entity.Cnpj = Cnpj;
            entity.Faturamento = Faturamento;
            entity.RamoId = (int)Ramo;

            return entity;
        }
    }
}
