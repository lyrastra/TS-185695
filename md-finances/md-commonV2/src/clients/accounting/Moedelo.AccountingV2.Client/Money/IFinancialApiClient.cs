using Moedelo.AccountingV2.Dto.AdvanceStatement;
using Moedelo.AccountingV2.Dto.FinancialOperations;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.Money
{
    public interface IFinancialApiClient : IDI
    {
        Task<string> GetIncomingMoneyNextNumber(int firmId, int userId, int year, string settlement);

        Task<FinancialOperationDto> GetByBaseDocumentIdAsync(int firmId, int userId, long documentBaseId);

        Task<int> GetLastOutgoingNumberAsync(int firmId, int userId);

        Task<int> GetLastIncomingNumberAsync(int firmId, int userId);

        Task<WorkerBalanceDto> GetWorkerBalanceAsync(int firmId, int userId, int workerId,
            DateTime advanceStatementDate,
            long? advanceStatementId);

        Task<List<AdvanceStatementFinancialObjectDto>> GetAdvanceDocumentsAsync(int firmId, int userId,
            long advanceStatementId);

        Task<AdvanceStatementFinancialObjectDto> GetAdvanceDocumentByBaseIdAsync(int firmId, int userId,
            long advanceDocumentBaseId);

        Task<List<AdvanceStatementFinancialObjectDto>> GetDebtDocumentsAsync(int firmId, int userId,
            long advanceStatementId);

        /// <summary>
        /// Получение ВСЕХ несвязанных с АО платежей на выдачу аванса
        /// </summary>
        /// <param name="firmId">Фирма</param>
        /// <param name="userId">Пользователь</param>
        /// <param name="workerId">Фильтр по сотруднику</param>
        /// <param name="date">Фильтр по дате: до указанной даты включительно</param>
        /// <param name="advanceDocumentId">Фильтр по АО: исключить из связей указанный АО (опционально)</param>
        /// <param name="takeCount">Вернуть указанное кол-во самых свежих платежей (опционально) </param>
        Task<List<AdvanceStatementFinancialObjectDto>> GetAllAdvanceDocumentsAsync(
            int firmId,
            int userId,
            int workerId,
            DateTime date,
            long? advanceDocumentId = null,
            int? takeCount = null);

        Task<List<AdvanceStatementFinancialObjectDto>> GetAllDebtDocumentsAsync(int firmId, int userId, int workerId,
            long advanceStatementId, DateTime date,
            PrimaryDocumentsTransferDirection direction);

        Task<AdvanceStatementFinancialDto> ProvideAdvanceStatementFinancialObjectsAsync(int firmId, int userId, AdvanceStatementFinancialDto dto);

        Task ProvideAccountingPostingsAsync(int firmId, int userId, long advanceStatementId);
    }
}