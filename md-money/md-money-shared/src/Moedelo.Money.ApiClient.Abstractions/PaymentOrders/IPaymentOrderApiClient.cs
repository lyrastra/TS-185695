using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Snapshot;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders
{
    public interface IPaymentOrderApiClient
    {
        Task<OperationType> GetTypeAsync(long documentBaseId);

        Task<OperationTypeDto[]> GetTypeByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds);
        
        Task ProvideAsync(IReadOnlyCollection<long> documentBaseIds);

        Task ApproveImportedAsync(ApproveImportedOperationsRequestDto dto);

        /// <summary>
        /// Получение базовых идентификаторов безналичных денежных операций по типу операции
        /// Используется для перепроведения при отключении / включении склада
        /// Если год не передать то будут получены операции за текущий год
        /// </summary>
        Task<IReadOnlyCollection<long>> GetBaseIdsByOperationTypeAsync(OperationType operationType, int? year = null);

        /// <summary>
        /// Для вычисления номера новой исходящей денежной операции
        /// </summary>
        /// <param name="settlementAccountId">Идентификатор расчетного счета</param>
        /// <param name="year">Расчетный год(если не указан то берётся текущий)</param>
        /// <param name="cut">Значение после которого делается выборка(если не указан то с нуля)</param>
        /// <returns>Список номеров платёжек созданных в указаном году</returns>
        Task<IReadOnlyCollection<int>> GetOutgoingNumbersAsync(int settlementAccountId, int? year = null, int? cut = null);

        /// <summary>
        /// Получение snapshot'а платежа
        /// </summary>
        Task<PaymentOrderSnapshotDto> GetPaymentOrderSnapshotAsync(
            long documentBaseId,
            CancellationToken ct);
    }
}