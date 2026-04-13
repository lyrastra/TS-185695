using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Bills.Dto.Marketplace;

namespace Moedelo.Billing.Abstractions.Bills.Interfaces;

public interface IMarketplaceApiClient
{
    Task<PackageProlongationInfoDto> GetProlongationInfoAsync(ProlongationRequestDto request);
}