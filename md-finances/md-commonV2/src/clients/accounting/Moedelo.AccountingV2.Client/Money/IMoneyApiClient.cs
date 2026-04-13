using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.FinancialOperations.Legasy;
using Moedelo.AccountingV2.Dto.Money;
using Moedelo.Common.Enums.Enums.FinancialOperations;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.Money
{
    public interface IMoneyApiClient : IDI
    {
        Task<string> GetOutgoingMoneyNextNumberAsync(int firmId, int userId, int year, string settlement);

        Task<int> GetOutgoingMoneyNextNumberBySettlementIdAsync(int firmId, int userId, int settlementAccountId);

        Task DeleteOperations(int firmId, int userId, IReadOnlyCollection<long> baseIds);
        
        Task<List<long>> GetTradingObjectPaymentByIdsAsync(int firmId, int userId, IReadOnlyCollection<long> ids);

        Task<long> CreateTradingObjectPaymentAsync(int firmId, int userId, TradingObjectOperationDto requestDto);

        Task<PatentPaymentOrderDto> GetPatentPaymentOrderAsync(int firmId, int userId, long id);

        Task<List<IncomingOutgoingSumDto>> GetSumForIncomingAndOutgoingOperationsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds);

        Task ReplaceKontragentInMoneyAndCashAsync(int firmId, int userId, KontragentForMoneyReplaceDto request);

        Task<bool> HasPurseOperationsAsync(int firmId, int userId, int purseId);

        Task<int> ProvideFinancialOperationAsync(int firmId, int userId, FinancialOperationDto dto, FinancialOperationSource source);

        Task<FinancialOperationDto[]> GetFinancialOperationsAsync(int firmId, int userId, IReadOnlyCollection<int> ids);

        Task DeleteOperationsByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids, FinancialOperationSource source);

        Task<int> UpdateDateInOperationAsync(int firmId, int userId, int operationId, DateTime date, FinancialOperationSource source);

        Task SaveTradingObjectPaymentLinkAsync(int firmId, int userId, TradingObjectPaymentLinkDto link);

        Task SavePatentPaymentOrderAsync(int firmId, int userId, PatentPaymentOrderDto dto);

        Task<MoneyTransferDto[]> GetDuplicateIncomingYandexTransfersAsync(int firmId, int userId, double sum, DateTime date, int kontragentId, string description);

        Task<MoneyTransferDto[]> GetDuplicateOutgoingYandexTransfersAsync(int firmId, int userId, double sum, DateTime date, int kontragentId, string description);

        Task<MoneyTransferDto[]> GetDuplicateMovementYandexTransfersAsync(int firmId, int userId, double sum, DateTime date, string documentNumber);

        Task<MoneyTransferDto[]> GetDuplicateIncomingForTypeTransferAsync(int firmId, int userId, DateTime date, double summ, string operationType, int? settlementAccountId, int? kontragentId);

        Task<MoneyTransferDto[]> GetDuplicateIncomingTransferAsync(int firmId, int userId, DateTime date, double summ, string paymentNumber, int? settlementAccountId, int? kontragentId);

        Task<List<int>> GetUsingSettlementAccountIdsForKudirAsync(int firmId, int userId, DateTime startDate, DateTime endDate);

        Task<long?> GetPatentIdByOperationAsync(int firmId, int userId, long operationBaseId);

        Task<MoneyTransferOperationNds> GetMoneyTransferOperationNdsAsync(int firmId, int userId, long moneyBaseId);

        Task<double> GetIncomingTransferUsnSumAsync(int firmId, DateTime startDate, DateTime endDate);

        Task<double> GetIncomingTransferUsnSumByProvisionOfServicesAsync(int firmId, DateTime startDate, DateTime endDate);
        
        Task<double> GetOutgoingTransferUsnSumByRefundToCustomerOutgoingOperationAsync(int firmId, DateTime startDate, DateTime endDate);

        Task<List<FundsBalanceSumDto>> GetBudgetaryOperationsForMoneyBalanceMasterFundsBalanceAsync(int firmId);
    }
}
