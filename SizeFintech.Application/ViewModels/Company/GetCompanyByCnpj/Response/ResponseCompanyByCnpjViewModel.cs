
using SizeFintech.Application.ViewModels.Invoice.GetInvoiceByCnpj.Response;
using SizeFintech.Domain.Models.Entities;

namespace SizeFintech.Application.ViewModels.Company.GetCompanyByCnpj.Response
{
    public class ResponseCompanyByCnpjViewModel
    {
        public string Empresa { get; set; }
        public string Cnpj { get; set; }
        public decimal Limite { get; set; }
        public IEnumerable<ResponseInvoiceByCnpjViewModel> NotasFiscais { get; set; }
        public decimal TotalLiquido { get; set; }
        public decimal TotalBruto { get; set; }

        public  ResponseCompanyByCnpjViewModel ToViewModel(CompanyEntity company)
        {
            return new ResponseCompanyByCnpjViewModel
            {
                Empresa = company.Nome,
                Cnpj = company.Cnpj,
                Limite = company.LimiteCredito,
                NotasFiscais = company.NotasFiscais?.Select(nf => new ResponseInvoiceByCnpjViewModel
                {
                    ValorBruto = nf.ValorBruto,
                    ValorLiquido = nf.ValorLiquido!.Value,
                }) ?? Enumerable.Empty<ResponseInvoiceByCnpjViewModel>(),
                TotalBruto = company.Carrinho?.TotalBruto ?? 0,
                TotalLiquido = company.Carrinho?.TotalLiquido ?? 0
            };
        }
    }
}
