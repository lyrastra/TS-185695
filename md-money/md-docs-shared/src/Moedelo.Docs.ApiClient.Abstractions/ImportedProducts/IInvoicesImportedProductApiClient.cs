using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.ImportedProducts.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.ImportedProducts
{
    public interface IInvoicesImportedProductApiClient
    {
        Task<List<ImportedProductDto>> GetAllDeclarationsForProductAsync(long stockProductId);
    }
}