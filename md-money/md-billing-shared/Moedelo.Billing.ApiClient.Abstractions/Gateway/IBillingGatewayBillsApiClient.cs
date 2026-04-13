using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.Gateway.Bills;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.Abstractions.Gateway;

public interface IBillingGatewayBillsApiClient
{
    Task<BillingGatewayBillInfoDto> GetBillInfoAsync(
        string billNumber,
        HttpQuerySetting setting = null);
}
