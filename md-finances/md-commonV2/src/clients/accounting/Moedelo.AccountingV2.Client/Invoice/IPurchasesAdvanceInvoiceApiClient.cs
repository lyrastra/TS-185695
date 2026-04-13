using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.AdvanceInvoice;

namespace Moedelo.AccountingV2.Client.Invoice
{
    /// <summary>
    /// Клиент для работы с авансовыми СФ из раздела Покупки
    /// </summary>
    public interface IPurchasesAdvanceInvoiceApiClient
    {
        Task<List<PurchasesAdvanceInvoiceDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds, CancellationToken cancellationToken);
    }
}
