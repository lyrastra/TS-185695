using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Providing.ApiClient.Abstractions.ProvidingState;
using Moedelo.Providing.ApiClient.Abstractions.ProvidingState.Models;
using Moedelo.Providing.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Providing
{
    [InjectAsSingleton(typeof(PaymentOrderProvidingStateSetter))]
    internal sealed class PaymentOrderProvidingStateSetter
    {
        private readonly ILogger logger;
        private readonly IProvidingStateApiClient providingStateApiClient;

        public PaymentOrderProvidingStateSetter(
            ILogger<PaymentOrderProvidingStateSetter> logger,
            IProvidingStateApiClient providingStateApiClient)
        {
            this.logger = logger;
            this.providingStateApiClient = providingStateApiClient;
        }

        public async Task<long> SetStateAsync(long documentBaseId)
        {
            try
            {
                return await providingStateApiClient.SetAsync(
                    new SetStateRequestDto
                    {
                        DocumentBaseId = documentBaseId,
                        Type = ProvidingStateType.All
                    },
                    setting: new HttpQuerySetting(TimeSpan.FromSeconds(30)));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Unable to set providing state {ProvidingStateType.All} for documentBaseId {documentBaseId}");
            }
            return 0;
        }

        public async Task<long> SetCustomTaxPostingsStateAsync(long documentBaseId)
        {
            try
            {
                return await providingStateApiClient.SetAsync(
                    new SetStateRequestDto
                    {
                        DocumentBaseId = documentBaseId,
                        Type = ProvidingStateType.CustomTaxPostings
                    },
                    setting: new HttpQuerySetting(TimeSpan.FromSeconds(30)));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Unable to set providing state {ProvidingStateType.CustomTaxPostings} for documentBaseId {documentBaseId}");
            }
            return 0;
        }

        public Task UnsetStateAsync(long stateId)
        {
            if (stateId == 0)
            {
                return Task.CompletedTask;
            }

            return providingStateApiClient.UnsetAsync(stateId);
        }
    }
}
