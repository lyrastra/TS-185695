using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Finances.Domain.Models.Kontragents;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.Finances.Domain.Models;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money
{
    public interface IMoneyOperationReader : IDI
    {
        Task<DateTime?> GetLastOperationDateUntilAsync(int firmId, DateTime date);
        Task<List<KontragentTurnover>> TopByOperationsWithKontragentsAsync(IUserContext userContext, int count, DateTime startDate, DateTime endDate);
        Task<long> GetDocumentBaseIdAsync(IUserContext userContext, long id);
        Task<List<PaymentOrderStatus>> GetStatusesByBaseIdsAsync(IUserContext userContext, IReadOnlyCollection<long> documentsBaseIdList);
        Task<bool> HasOperationsBySettlementAccountAsync(IUserContext userContext, int settlementAccountId);
        Task<List<OperationKindAndBaseId>> GetKindsByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Метод для поддержки на UI бэканда новых денег.
        /// Не использовать без острой необходимости
        /// </summary>
        Task<MoneyOperation> GetByBaseIdAsync(int firmId, long baseId);

        Task<MoneyOperation[]> GetByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Метод для генерации отчета.
        /// Не использовать без острой необходимости
        /// </summary>
        Task<MoneyOperation[]> GetByPeriodAsync(int firmId, Period period);

        /// <summary>
        /// Метод для получения операций по патенту
        /// </summary>
        Task<MoneyOperation[]> GetByPatentAsync(int firmId, long patentId);
    }
}
