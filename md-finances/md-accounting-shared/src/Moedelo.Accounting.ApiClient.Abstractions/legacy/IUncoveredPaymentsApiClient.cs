using System;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// Реестр недостающих документов
    /// </summary>
    public interface IUncoveredPaymentsApiClient
    {
        /// <summary>
        /// Скачивает файл реестра за период
        /// </summary>
        Task<HttpFileModel> DownloadReportAsync(FirmId firmId, UserId userId, UncoveredPaymentsRequestDto request);

        /// <summary>
        /// Есть ли операции не покрытые документами
        /// </summary>
        Task<bool> HasUncoveredOperationsAsync(FirmId firmId, UserId userId, DateTime startDate, DateTime endDate);
    }
}