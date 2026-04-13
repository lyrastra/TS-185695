using System;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Payments;
using Moedelo.AccountingV2.Dto.Payments.Requests;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.Payments
{
    public interface IBizPaymentsApiClient : IDI
    {
        Task<WorkerPaymentWithNdflDto> GetWorkerPaymentWithNdflAsync(int firmId, int userId, DateTime startDate,
            DateTime endDate, DateTime chargedStartDate);

        Task<WorkerPaymentWithNdflDto> GetNdflPaymentsAsync(int firmId, int userId, DateTime startDate,
            DateTime endDate, DateTime chargedStartDate);

        Task<WorkerRefundFromBudgetPaymentWithNdflDto> GetNdflRefundFromBudgetPaymentsAsync(int firmId, int userId, DateTime startDate, DateTime endDate);

        Task<SalaryPaymentsDto> GetSalaryPaymentsAsync(int firmId, int userId, GetSalaryPaymentsRequestDto request);

        Task<DocumentsNumbersDto> GetPaymentDocumentLastNumbersAsync(int firmId, int userId,
            GetPaymentDocumentLastNumbersRequestDto request);

        Task<SavedPaymentsDocumentResultDto> SavePaymentDocumentsAsync(int firmId, int userId,
            SavingPaymentsModelDto data);

        Task<FundPaymentsDto> GetFundPaymentsAsync(int firmId, int userId, GetFundPaymentsRequestDto request);

        Task<SavedPaymentsDocumentResultDto> SaveFundPaymentAsync(int firmId, int userId, SavingFundPaymentDto request);

        Task<PaymentsForReportDto> GetFundPaymentsForReportAsync(int firmId, int userId,
            GetFundPaymentsForReportRequestDto request);

        Task<RefundFromBudgetPaymentsDto> GetRefundFromBudgetPayments(int firmId, int userId, GetFundPaymentsForReportRequestDto request);

        Task<PaymentByWorkerDto> GetWorkerPaymentsAsync(int firmId, int userId, int workerId, DateTime startDate,
            DateTime endDate);

        Task<bool> HasDependenciesByWorkerAsync(int firmId, int userId, int workerId);

        Task<AccountingBalanceForPaymentDto> GetBalancesAsync(int firmId, int userId, DateTime date);

        Task<PaymentsForReportDto> GetFundMoneyForReportAsync(int firmId, int userId,
            GetFundBizPaymentsRequestDto request);

        Task<DateTime?> GetBalancesDateAsync(int firmId, int userId);
    }
}
