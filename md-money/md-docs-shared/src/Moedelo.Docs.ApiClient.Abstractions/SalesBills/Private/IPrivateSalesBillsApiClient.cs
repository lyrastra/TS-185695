using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.SalesBills.Private.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesBills.Private
{
    public interface IPrivateSalesBillsApiClient
    {
        Task<IReadOnlyList<FetchBillModelDto>> FetchAsync(FetchBillsRequestDto request,CancellationToken ct);
    }
}