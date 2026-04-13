using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Legacy.Dto.ProductSeller;

namespace Moedelo.Billing.Abstractions.Legacy.Interfaces.ProductSeller;

public interface IProductSellerApiClient
{
    Task<IReadOnlyCollection<ProductSellerDto>> GetSellersAsync(IReadOnlyCollection<string> productCodes);
    Task<IReadOnlyCollection<ProductSellerDto>> GetProductsAsync(string sellerCode);
}