using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Finances.Client.Money.Dtos;
using Moedelo.Finances.Dto.Kontragents;
using Moedelo.Finances.Dto.Money;
using Moedelo.Finances.Dto.Money.Operations;
using Moedelo.Finances.Dto.Money.Operations.Requests;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Client.Money
{
    public interface IMoneyOperationsClient : IDI
    {
        /// <summary>
        /// Возвращает топ денежных оборотов по операциям с контрагентами (за период)
        /// </summary>
        Task<List<KontragentTurnoverDto>> TopByOperationsWithKontragents(int firmId, int userId, int count, DateTime startDate, DateTime endDate);

        Task<long> GetDocumentBaseId(int firmId, int userId, long id);

        /// <summary>
        /// Получить статусы оплат для платёжных поручений по DocumentBaseId
        /// </summary>
        Task<List<PaymentOrderStatusDto>> GetStatusesByBaseIds(int firmId, int userId,
            IReadOnlyCollection<long> documentsBaseIds, CancellationToken cancellationToken = default);

        Task<bool> HasOperationsBySettlementAccountAsync(int firmId, int userId, int settlementAccountId);

        /// <summary>Получить бюджетные платежи</summary>
        Task<IEnumerable<BudgetaryPaymentForReportDto>> GetBudgetaryPaymentsAsync(int firmId, int userId, GetBudgetaryAccPaymentsRequestDto request);

        /// <summary>
        /// Получить бюджетные платежи (вер.2), расширен запрос по AccountCode:
        /// - в запросе BudgetaryTaxesAndFees(AccountCode) можно списком задавать;
        /// </summary>
        Task<IEnumerable<BudgetaryPaymentForReportDto>> GetBudgetaryPaymentsV2Async(int firmId, int userId, BudgetaryAccPaymentsRequestDto request);
        
        /// <summary>Получить авансовые платежей по УСН</summary>
        Task<List<UsnBudgetaryPrepaymentDto>> GetUsnBudgetaryPrepaymentsAsync(int firmId, int userId, int year, bool needCashOperations);

        /// <summary>Удалить платёжные операций из денег (PaymentOrderOperation или CashOrderOperation)</summary>
        Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);
        
        /// <summary>
        /// Получить типы операций (с расчетного счёта, наличными, через электронный кошелёк) по DocumentBaseId
        /// </summary>
        Task<List<MoneySourceTypeAndBaseIdDto>> GetTypesByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentsBaseIds);

        /// <summary>
        /// Получить состояние операций по DocumentBaseId
        /// </summary>
        Task<List<PaymentOrderOperationsStateDto>> GetOperationsStateByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentsBaseIds);

        /// <summary>
        /// Получить операции по DocumentBaseId
        /// </summary>
        Task<List<MoneyOperationDto>> GetOperationsByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentsBaseIds);
    }
}