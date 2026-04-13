using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// Информация о покрытии платежей
    /// </summary>
    public interface IPaymentCoverageApiClient
    {
        /// <summary>
        /// Возвращает сводные данные о покрытии платежей для указанных фирм
        /// </summary>
        Task<IReadOnlyList<PaymentCoverageSummaryDto>> GetSummaryAsync(
            PaymentCoverageSummaryRequestDto request,
            HttpQuerySetting httpQuerySetting = null,
            CancellationToken ct = default);
    }
}