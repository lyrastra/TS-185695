using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Bills.Simple.PurchasesInvoice;
using Moedelo.AccountingV2.Dto.Invoices.Purchases;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.Invoice
{
    public interface IPurchasesInvoiceApiClient : IDI
    {
        Task<PurchasesCommonInvoiceCollectionDto> GetAsync(
            int firmId,
            int userId,
            uint pageNo = 1,
            uint pageSize = 50,
            string number = null,
            DateTime? docAfterDate = null,
            DateTime? docBeforeDate = null,
            DateTime? afterDate = null,
            DateTime? beforeDate = null,
            int? kontragentId = null);

        Task<List<PurchasesInvoiceSimpleDto>> GetWithItemsAsync(int firmId,
            int userId,
            uint pageNo = 1,
            uint pageSize = 50,
            string number = null,
            DateTime? docAfterDate = null,
            DateTime? docBeforeDate = null,
            DateTime? afterDate = null,
            DateTime? beforeDate = null,
            int? kontragentId = null,
            CancellationToken cancellationToken = default);

        Task<PurchasesCommonInvoiceDto> GetByBaseIdAsync(int firmId, int userId, long baseId);

        Task<List<PurchasesCommonInvoiceDto>> GetByBaseIdsWithItemsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
        
        Task<PurchasesCommonInvoiceDto> SaveAsync(int firmId, int userId, PurchasesCommonInvoiceSaveRequestDto dto);
    }
}
