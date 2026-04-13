using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Waybills;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Waybills.Dto;
using Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Purchases.Events;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Docs.Common.PurchasesWaybill;

[InjectAsSingleton]
internal sealed class PurchasesWaybillStatePreparer : IPurchasesWaybillStatePreparer
{
    private readonly IExecutionInfoContextAccessor contextAccessor;
    private readonly IWaybillApiClient apiClient;

    public PurchasesWaybillStatePreparer(IExecutionInfoContextAccessor contextAccessor, IWaybillApiClient apiClient)
    {
        this.contextAccessor = contextAccessor;
        this.apiClient = apiClient;
    }

    public async Task<PurchaseWaybillNewState> RestoreTruncatedDataAsync(PurchaseWaybillNewState state)
    {
        if (state == null || !state.IsTruncated)
        {
            return state;
        }

        var waybills = await apiClient.GetByBaseIdsAsync(
            contextAccessor.ExecutionInfoContext.FirmId,
            contextAccessor.ExecutionInfoContext.UserId,
            new[] { state.DocumentBaseId });

        state.Items = waybills
            ?.FirstOrDefault()
            ?.Items
            ?.Select(MapItem)
            .ToArray() ?? Array.Empty<PurchaseWaybillNewState.Item>();

        return state;
    }
    
    private static PurchaseWaybillNewState.Item MapItem(WaybillItemDto dto)
    {
        return new PurchaseWaybillNewState.Item
        {
            Name = dto.Name,
            Count = dto.Count,
            Price = dto.Price,
            Unit = dto.Unit,
            NdsSum = dto.NdsSum,
            NdsType = dto.NdsType,
            IsCustomSums = dto.IsCustomSums,
            StockProductId = dto.StockProductId.GetValueOrDefault(),
            SumWithNds = dto.SumWithNds,
            SumWithoutNds = dto.SumWithoutNds,
            RealCount = dto.RealCount,
        };
    }
}