using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Bills.Simple.SalesInvoice;
using Moedelo.AccountingV2.Dto.Invoices.Sales;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.Invoice
{
    public interface ISalesInvoiceApiClient : IDI
    {
        Task<SalesInvoiceCollectionDto> GetAsync(
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

        Task<List<SalesInvoiceSimpleDto>> GetWithItemsAsync(int firmId,
            int userId,
            uint pageNo = 1,
            uint pageSize = 50,
            string number = null,
            DateTime? docAfterDate = null,
            DateTime? docBeforeDate = null,
            DateTime? afterDate = null,
            DateTime? beforeDate = null,
            int? kontragentId = null);

        Task<SalesInvoiceDto> GetByBaseIdAsync(int firmId, int userId, long baseId);

        Task<List<SalesInvoiceDto>> GetByBaseIdsWithItemsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        Task<SalesInvoiceDto> SaveAsync(int firmId, int userId, SalesInvoiceSaveRequestDto dto);
    }
}
