using System.Threading;
using Moedelo.Docs.Dto.Common;
using Moedelo.Docs.Dto.ProductMerge;
using Moedelo.Docs.Dto.SalesCurrencyInvoices;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.Docs.Client.SalesCurrencyInvoices
{
    public interface ISalesCurrencyInvoicesApiClient : IDI
    {
        Task<ReportFileDto> GetFileAsync(int firmId, int userId, long id, bool useStampAndSign);

        Task<SalesCurrencyInvoiceDto[]> GetByPeriodAsync(int firmId, int userId, PeriodRequestDto request, CancellationToken cancellationToken = default);

        Task MergeItemsAsync(int firmId, int userId, ProductMergeRequestDto mergeRequest);
    }
}