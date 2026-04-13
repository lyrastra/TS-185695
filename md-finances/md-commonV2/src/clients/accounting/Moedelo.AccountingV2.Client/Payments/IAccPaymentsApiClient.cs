using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Payments;
using Moedelo.AccountingV2.Dto.Payments.Requests;

namespace Moedelo.AccountingV2.Client.Payments
{
    public interface IAccPaymentsApiClient
    {
        Task<WorkerPaymentWithNdflDto> GetWorkerPaymentWithNdflAsync(int firmId, int userId, DateTime startDate, DateTime endDate, DateTime chargedStartDate);

        Task<WorkerPaymentWithNdflDto> GetNdflPaymentsAsync(int firmId, int userId, DateTime startDate, DateTime endDate, DateTime chargedStartDate);

        Task<WorkerRefundFromBudgetPaymentWithNdflDto> GetNdflRefundFromBudgetPaymentsAsync(int firmId, int userId, DateTime startDate, DateTime endDate);

        Task<SalaryPaymentsDto> GetSalaryPaymentsAsync(int firmId, int userId, GetSalaryPaymentsRequestDto request);

        Task<DocumentsNumbersDto> GetPaymentDocumentLastNumbersAsync(int firmId, int userId, GetPaymentDocumentLastNumbersRequestDto request);

        Task<SavedPaymentsDocumentResultDto> SavePaymentDocumentsAsync(int firmId, int userId, SavingPaymentsModelDto data);

        Task<FundPaymentsDto> GetFundPaymentsAsync(int firmId, int userId, GetFundPaymentsRequestDto request);

        Task<SavedPaymentsDocumentResultDto> SaveFundPaymentAsync(int firmId, int userId, SavingFundPaymentDto request);

        Task<PaymentsForReportDto> GetFundPaymentsForReportAsync(int firmId, int userId, GetFundPaymentsForReportRequestDto request);

        Task<RefundFromBudgetPaymentsDto> GetRefundFromBudgetPaymentsAsync(int firmId, int userId, GetFundPaymentsForReportRequestDto request);

        Task<PaymentByWorkerDto> GetWorkerPaymentsAsync(int firmId, int userId, int workerId, DateTime startDate, DateTime endDate);

        Task<bool> HasDependenciesByWorkerAsync(int firmId, int userId, int workerId);

        Task<AccountingBalanceForPaymentDto> GetBalancesAsync(int firmId, int userId, DateTime date, DateTime? fromDate = null);

        Task<List<DeductionPaymentDto>> GetDeductionPaymentsAsync(int firmId, int userId, DateTime endDate);

        Task<PaymentsForReportDto> GetFundMoneyForReportAsync(int firmId, int userId, GetFundAccPaymentsRequestDto request);

        Task<SavedWorkerPaymentDto> GetWorkerPaymentByDocumentBaseIdAsync(int firmId, int userId, long documentBaseId);
        
        Task<PaymentByWorkerDto> GetWorkersPaymentsAsync(int firmId, int userId, DateTime startDate, DateTime endDate);
    }
}