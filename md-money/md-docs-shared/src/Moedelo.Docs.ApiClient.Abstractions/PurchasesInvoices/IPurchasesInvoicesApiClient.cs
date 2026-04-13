using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesInvoices.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesInvoices
{
    public interface IPurchasesInvoicesApiClient
    {
        Task<DataPageResponse<DocsPurchasesInvoiceByCriteriaResponseDto>> GetByCriteriaAsync(
            DocsPurchasesInvoicesByCriteriaRequestDto criteria, 
            int? companyId = null);
    }
}