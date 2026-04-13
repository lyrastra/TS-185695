using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards;
using Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Providing.Business.Estate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.Estate
{
    [InjectAsSingleton(typeof(InventoryCardReader))]
    internal class InventoryCardReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IInventoryCardApiClient inventoryCardApiClient;

        public InventoryCardReader(
            IExecutionInfoContextAccessor contextAccessor,
            IInventoryCardApiClient inventoryCardApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.inventoryCardApiClient = inventoryCardApiClient;
        }

        public async Task<InventoryCard[]> GetByBaseIdsAsync(IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.Count == 0)
            {
                return Array.Empty<InventoryCard>();
            }

            var context = contextAccessor.ExecutionInfoContext;
            var inventoryCards = await inventoryCardApiClient.GetByBaseIdsAsync(context.FirmId, context.UserId, baseIds).ConfigureAwait(false);
            return inventoryCards.Select(MapInventoryCard).ToArray();
        }

        public async Task<Dictionary<long, InventoryCard>> GetByPrimaryDocumentBaseIdsAsync(IReadOnlyCollection<long> primaryDocumentBaseIds)
        {
            if (primaryDocumentBaseIds.Count == 0)
            {
                return new Dictionary<long, InventoryCard>();
            }

            var context = contextAccessor.ExecutionInfoContext;
            var result = await inventoryCardApiClient.GetByPrimaryDocumentBaseIdsAsync(context.FirmId, context.UserId, primaryDocumentBaseIds).ConfigureAwait(false);
            return result.ToDictionary(x => x.Key, x => MapInventoryCard(x.Value));
        }

        private InventoryCard MapInventoryCard(InventoryCardDto dto)
        {
            return new InventoryCard
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                Cost = dto.TaxDescription.Cost,
                PaidCost = dto.TaxDescription.PaidCost,
                CommissioningDate = dto.TaxDescription?.CommissioningDate
            };
        }
    }
}
