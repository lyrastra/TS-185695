using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards;
using Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment
{
    [InjectAsSingleton(typeof(InventoryCardReader))]
    internal sealed class InventoryCardReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IInventoryCardApiClient client;

        public InventoryCardReader(IExecutionInfoContextAccessor contextAccessor, IInventoryCardApiClient client)
        {
            this.contextAccessor = contextAccessor;
            this.client = client;
        }

        public async Task<InventoryCard> GetByBaseIdAsync(long baseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var response = await client.GetByBaseIdsAsync(context.FirmId, context.UserId, new[] { baseId });
            return response.Any() ? Map(response.First()) : null;
        }

        private static InventoryCard Map(InventoryCardDto dto)
        {
            return new InventoryCard
            {
                DocumentBaseId = dto.DocumentBaseId,
                FixedAssetName = dto.FixedAssetName,
                InventoryNumber = dto.InventoryNumber
            };
        }
    }
}
