using Moedelo.Docs.Dto.Common;
using Moedelo.Docs.Dto.ProductMerge;
using Moedelo.Docs.Dto.PurchasesCurrencyInvoices;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Docs.Client.PurchasesCurrencyInvoices
{
    public interface IPurchasesCurrencyInvoicesApiClient
    {
        Task<PurchasesCurrencyInvoiceDto[]> GetByPeriodAsync(int firmId, int userId, PeriodRequestDto request,
            CancellationToken cancellationToken = default);

        Task ReprovideAsync(int firmId, int userId, IReadOnlyCollection<long> purchasesCurrencyInvoicesIds);

        Task MergeItemsAsync(int firmId, int userId, ProductMergeRequestDto mergeRequest);
    }
}