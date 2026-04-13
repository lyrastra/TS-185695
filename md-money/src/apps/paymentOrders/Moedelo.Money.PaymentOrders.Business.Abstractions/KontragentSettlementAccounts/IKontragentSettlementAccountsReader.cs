using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.KontragentSettlementAccounts
{
    public interface IKontragentSettlementAccountsReader
    {
        Task<List<KontragentSettlementAccountDto>> GetByKontragentAsync(int kontragentId);
    }
}
