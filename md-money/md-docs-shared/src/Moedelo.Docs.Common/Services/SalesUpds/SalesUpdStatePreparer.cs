using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Docs.ApiClient.Abstractions.SalesUpds;
using Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Sales;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Docs.Common.Services.SalesUpds;

[InjectAsSingleton]
internal sealed class SalesUpdStatePreparer : ISalesUpdStatePreparer
{
    private readonly IExecutionInfoContextAccessor contextAccessor;
    private readonly ISalesUpdsApiClient salesUpdsApiClient;

    public SalesUpdStatePreparer(
        IExecutionInfoContextAccessor contextAccessor, 
        ISalesUpdsApiClient salesUpdsApiClient)
    {
        this.contextAccessor = contextAccessor;
        this.salesUpdsApiClient = salesUpdsApiClient;
    }

    public async Task<SaleUpdNewState> RestoreTruncatedDataAsync(SaleUpdNewState state)
    {
        if (state == null || !state.IsTruncated)
        {
            return state;
        }
        
        var dto = await salesUpdsApiClient.GetWithItemsAsync(
            (int) contextAccessor.ExecutionInfoContext.FirmId,
            (int) contextAccessor.ExecutionInfoContext.UserId,
            new[] { state.DocumentBaseId });

        state.Items = dto
            ?.FirstOrDefault()
            ?.Items
            ?.Select(i => new SaleUpdNewState.Item
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
                Country = i.Country,
                Declaration = i.Declaration,
                NdsDeclarationSection7CodeId = i.NdsDeclarationSection7CodeId,
                Labels = i.Labels
                    ?.Select(l => new SaleUpdNewState.ProductLabel()
                    {
                        Code = l.Code,
                        Type = l.Type
                    })
                    .ToArray() ?? Array.Empty<SaleUpdNewState.ProductLabel>()
            })
            .ToArray() ?? Array.Empty<SaleUpdNewState.Item>();

        return state;
    }
}