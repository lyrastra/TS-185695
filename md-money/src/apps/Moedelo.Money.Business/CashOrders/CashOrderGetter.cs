using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.CashOrders;
using Moedelo.Money.Business.CashOrders.ApiClient;
using Moedelo.Money.CashOrders.Dto.CashOrders;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders
{
    [InjectAsSingleton(typeof(ICashOrderGetter))]
    internal sealed class CashOrderGetter : ICashOrderGetter
    {
        private readonly ICashOrderApiClient apiClient;

        public CashOrderGetter(
            ICashOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<OperationType> GetOperationTypeAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<OperationTypeDto>($"CashOrders/{documentBaseId}/OperationType").ConfigureAwait(false);
            return dto.OperationType;
        }

        public async Task<OperationTypeResponse[]> GetOperationTypeByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds.Count == 0)
            {
                return Array.Empty<OperationTypeResponse>();
            }
            var uniqueDocumentBaseIds = documentBaseIds.Distinct().ToArray();
            var response = await apiClient.GetOperationTypeByBaseIdsAsync(uniqueDocumentBaseIds);
            return response.Select(MapOperationType).ToArray();
        }

        private static OperationTypeResponse MapOperationType(OperationTypeDto dto)
        {
            return new OperationTypeResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                OperationType = dto.OperationType
            };
        }

        public async Task<long> GetOperationIdAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<OperationIdDto>($"CashOrders/{documentBaseId}/Id");
            return dto.OperationId;
        }
    }
}
