using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.AdvanceInvoice;

namespace Moedelo.AccountingV2.Client.Invoice
{
    /// <summary>
    /// Клиент для работы с авансовыми СФ из раздела Продажи
    /// </summary>
    public interface ISalesAdvanceInvoiceApiClient
    {
        Task<SalesAdvanceInvoiceDto> GetByBaseIdAsync(int firmId, int userId, long baseId);

        Task<List<SalesAdvanceInvoiceDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds, CancellationToken cancellationToken);
    }
}
