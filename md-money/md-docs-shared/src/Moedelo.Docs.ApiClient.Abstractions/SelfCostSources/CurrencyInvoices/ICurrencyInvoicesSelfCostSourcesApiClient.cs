using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Common;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.CurrencyInvoices.Models.Purchases;
using Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.CurrencyInvoices.Models.Sales;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.CurrencyInvoices
{
    public interface ICurrencyInvoicesSelfCostSourcesApiClient
    {
        /// <summary>
        /// Возвращает входящие валютные инвойсы для расчёта себестоимости
        /// </summary>
        Task<IReadOnlyCollection<PurchaseCurrencyInvoicesSelfCostDto>> GetPurchasesOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct);

        /// <summary>
        /// Возвращает исходящие валютные инвойсы для расчёта себестоимости
        /// </summary>
        Task<IReadOnlyCollection<SaleCurrencyInvoicesSelfCostDto>> GetSalesOnDateAsync(SelfCostSourceRequestDto request, CancellationToken ct);

        /// <summary>
        /// Возвращае признак наличия входящих валютных инвойсов за период
        /// </summary>
        Task<bool> HasPurchasesOnDateAsync(DateTime? startDate, DateTime endDate);
    }
}