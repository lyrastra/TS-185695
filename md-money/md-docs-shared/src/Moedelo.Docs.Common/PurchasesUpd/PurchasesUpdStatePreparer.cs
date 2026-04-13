using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Upds;
using Moedelo.Docs.Common.Services.SalesUpds;
using Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Purchases;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Docs.Common.PurchasesUpd;

[InjectAsSingleton]
internal sealed class PurchasesUpdStatePreparer : IPurchasesUpdStatePreparer
{
    private readonly IExecutionInfoContextAccessor contextAccessor;
    private readonly IPurchasesUpdApiClient updApiClient;

    public PurchasesUpdStatePreparer(
        IExecutionInfoContextAccessor contextAccessor, 
        IPurchasesUpdApiClient updApiClient)
    {
        this.contextAccessor = contextAccessor;
        this.updApiClient = updApiClient;
    }

    public async Task<PurchaseUpdNewState> RestoreTruncatedDataAsync(PurchaseUpdNewState state)
    {
        if (state == null || !state.IsTruncated)
        {
            return state;
        }
        
        var dtos = await updApiClient.GetWithItemsAsync(
            contextAccessor.ExecutionInfoContext.FirmId,
            contextAccessor.ExecutionInfoContext.UserId,
            new[] { state.DocumentBaseId });

        state.Items = dtos
            ?.FirstOrDefault()
            ?.Items
            ?.Select(i => new PurchaseUpdNewState.Item
            {
                Name = i.Name,
                Unit = i.Unit,
                Price = i.Price,
                Count = i.Count,
                SumWithoutNds = i.SumWithoutNds,
                SumWithNds = i.SumWithNds,
                NdsSum = i.NdsSum,
                IsCustomSums = i.IsCustomSums,
                StockProductId = i.StockProductId,
                NdsType = i.NdsType,
                NdsOperationType = i.NdsOperationType,
                ActivityAccountCode = i.ActivityAccountCode,
                GtdCountry = i.GtdCountry,
                GtdNumber = i.GtdNumber,
            })
            .ToArray() ?? Array.Empty<PurchaseUpdNewState.Item>();

        return state;
    }
}