using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationError;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationError;
using Moedelo.Finances.Business.Helpers.Integration;
using Moedelo.Finances.Domain.Interfaces.Business.Integrations;
using Moedelo.Finances.Domain.Models.Integrations;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.Finances.Business.Services.Integrations
{
    [InjectAsSingleton(typeof(IIntegrationErrorsService))]
    public class IntegrationErrorsService(ILogger logger,
        IIntegrationErrorApiClient integrationErrorClient) : IIntegrationErrorsService
    {
        private const string TAG = nameof(IntegrationErrorsService);

        public async Task<List<IntegrationErrorResponse>> GetIntegrationErrorsAsync(int firmId, CancellationToken ctx)
        {
            var integrationErrorDtos = await GetErrorsAsync(firmId, ctx).ConfigureAwait(false);
            if (integrationErrorDtos.Count == 0)
            {
                return new List<IntegrationErrorResponse>();
            }

            var integrationErrorResponses = integrationErrorDtos.GetIntegrationErrorResponses();
            return integrationErrorResponses;
        }

        public async Task SetIntegrationErrorAsReadAsync(int firmId, IReadOnlyCollection<int> errorIds)
        {
            var request = new ReadUnreadIntegrationErrorRequestDto
            {
                FirmId = firmId,
                Ids = errorIds.ToList()
            };
            await integrationErrorClient.ReadUnreadByIdsAsync(request).ConfigureAwait(false);
        }

        private async Task<List<IntegrationErrorDto>> GetErrorsAsync(int firmId, CancellationToken ctx)
        {
            var requestDto = new GetListIntegrationErrorRequestDto
            {
                FirmId = firmId
            };

            try
            {
                var integrationResponse = await integrationErrorClient
                    .GetListAsync(requestDto, ctx)
                    .ConfigureAwait(false);

                return integrationResponse.Items;
            }
            catch (Exception ex)
            {
                logger.Error(TAG, ex.Message, ex);
            }

            return [];
        }
    }
}