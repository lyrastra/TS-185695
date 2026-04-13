using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Waybills;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Waybills.Dto;
using Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Sales.Events;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Docs.Common.SalesWaybill;

[InjectAsSingleton]
internal sealed class SalesWaybillStatePreparer : ISalesWaybillStatePreparer
{
    private readonly IExecutionInfoContextAccessor contextAccessor;
    private readonly IWaybillApiClient apiClient;

    public SalesWaybillStatePreparer(IExecutionInfoContextAccessor contextAccessor, IWaybillApiClient apiClient)
    {
        this.contextAccessor = contextAccessor;
        this.apiClient = apiClient;
    }

    public async Task<SaleWaybillNewState> RestoreTruncatedDataAsync(SaleWaybillNewState state)
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
            .ToArray() ?? Array.Empty<SaleWaybillNewState.Item>();

        return state;
    }

    private static SaleWaybillNewState.Item MapItem(WaybillItemDto dto)
    {
        return new SaleWaybillNewState.Item
        {
            NdsType = dto.NdsType,
            DiscountRate = dto.DiscountRate,
            IsCustomSums = dto.IsCustomSums,
            StockProductId = dto.StockProductId.GetValueOrDefault(),
            SumWithNds = dto.SumWithNds,
            SumWithoutNds = dto.SumWithoutNds,
            Name = dto.Name,
            Count = dto.Count,
            Price = dto.Price,
            NdsSum = dto.NdsSum,
            Unit = dto.Unit,
            NdsDeclarationSection7CodeId = dto.NdsDeclarationSection7CodeId,
        };
    }
}