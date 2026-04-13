using Moedelo.Money.Common.Domain.Models.Reports;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Snapshot;
using System.Threading;

namespace Moedelo.Money.Business.PaymentOrders.ApiClient
{
    internal interface IPaymentOrderApiClient
    {
        Task<T> GetAsync<T>(string path) where T : class;
        
        Task CreateAsync<T>(string path, T dto) where T : class;
        
        Task UpdateAsync<T>(string path, T dto) where T : class;

        Task<TResponse> UpdateAsync<TRequest, TResponse>(string path, TRequest dto)
            where TRequest : class;

        Task DeleteAsync(string path);

        Task<TResponse> DeleteAsync<TResponse>(string path);

        Task<OperationTypeDto[]> GetOperationTypeByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds);

        Task<long[]> DeleteInvalidAsync(IReadOnlyCollection<long> documentBaseIds);

        Task ApplyIgnoreNumberAsync(IReadOnlyCollection<long> documentBaseIds);

        Task<PaymentOrderWithMissingEmployeeResponse[]> GetPaymentOrdersWithMissingEmployee();

        Task PostAsync<TRequest>(string uri, TRequest data) where TRequest : class;

        Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest data, CancellationToken cancellationToken = default) where TRequest : class;

        Task PutAsync<TRequest>(string uri, TRequest data) where TRequest : class;

        Task ApproveImportedAsync(int? settlementAccountId, DateTime? skipline);

        Task<IReadOnlyList<long>> GetBaseIdsByOperationTypeAsync(OperationType operationType, int? year);

        Task<ReportFile> DownloadFileAsync(string path);

        Task<IReadOnlyList<int>> GetOutgoingNumbersAsync(int settlementAccountId, int? year, int? cut);

        Task<bool> GetIsPaidAsync(long documentBaseId);

        /// <summary>
        /// Получает статусы документов по заданным параметрам запроса.
        /// </summary>
        /// <param name="request">
        /// Объект запроса, содержащий параметры для фильтрации документов: идентификаторы базовых документов, 
        /// статус проверки сотрудником аутсорсера и статус оплаты.
        /// </param>
        /// <param name="setting">
        /// Дополнительные HTTP-настройки запроса (необязательный параметр).
        /// </param>
        /// <returns>
        /// Асинхронная задача, возвращающая список статусов документов.
        /// </returns>
        Task<IReadOnlyList<DocumentStatus>> GetDocumentsStatusAsync(DocumentsStatusRequest request, 
            HttpQuerySetting setting = null);

        /// <summary>
        /// Получение snapshot'а платежа
        /// </summary>
        Task<PaymentOrderSnapshotDto> GetPaymentOrderSnapshotAsync(
            long documentBaseId,
            HttpQuerySetting setting = null);
    }
}
