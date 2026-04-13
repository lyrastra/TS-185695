using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BankIntegrationsV2.Client.SberbankSubscription
{
    public interface ISberbankSubscriptionPriceListClient: IDI
    {
        Task<int> GetPriceListIdByTariffAndMonthCountAsync(int tariffId, int monthCount);
    }
}
