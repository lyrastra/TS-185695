using System;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Payments;
using Moedelo.AccountingV2.Dto.Payments.Requests;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Payments
{
    [InjectAsSingleton]
    public class BizPaymentsApiClient : BaseApiClient, IBizPaymentsApiClient
    {
        private readonly HttpQuerySetting defaultSetting = new HttpQuerySetting(TimeSpan.FromMinutes(5));

        private readonly SettingValue apiEndPoint;

        public BizPaymentsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
                )
        {
            apiEndPoint = settingRepository.Get("BizApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<WorkerPaymentWithNdflDto> GetWorkerPaymentWithNdflAsync(int firmId, int userId,
            DateTime startDate, DateTime endDate, DateTime chargedStartDate)
        {
            return await GetAsync<WorkerPaymentWithNdflDto>(
                    "/MoneyPayments/GetWorkerPaymentWithNdfl",
                    new { firmId, userId, startDate, endDate, chargedStartDate }, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public async Task<WorkerPaymentWithNdflDto> GetNdflPaymentsAsync(int firmId, int userId, DateTime startDate,
            DateTime endDate, DateTime chargedStartDate)
        {
            return await GetAsync<WorkerPaymentWithNdflDto>(
                    "/MoneyPayments/GetNdflPayments",
                    new { firmId, userId, startDate, endDate, chargedStartDate }, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public async Task<WorkerRefundFromBudgetPaymentWithNdflDto> GetNdflRefundFromBudgetPaymentsAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return await GetAsync<WorkerRefundFromBudgetPaymentWithNdflDto>(
                    "/MoneyPayments/GetNdflRefundFromBudgetPayments",
                    new { firmId, userId, startDate, endDate }, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public async Task<SalaryPaymentsDto> GetSalaryPaymentsAsync(int firmId, int userId,
            GetSalaryPaymentsRequestDto request)
        {
            return await PostAsync<GetSalaryPaymentsRequestDto, SalaryPaymentsDto>(
                    $"/MoneyPayments/GetSalaryPayments?firmId={firmId}&userId={userId}",
                    request, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public async Task<DocumentsNumbersDto> GetPaymentDocumentLastNumbersAsync(int firmId, int userId,
            GetPaymentDocumentLastNumbersRequestDto request)
        {
            return await PostAsync<GetPaymentDocumentLastNumbersRequestDto, DocumentsNumbersDto>(
                    $"/MoneyPayments/GetPaymentDocumentLastNumbers?firmId={firmId}&userId={userId}",
                    request, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public async Task<SavedPaymentsDocumentResultDto> SavePaymentDocumentsAsync(int firmId, int userId,
            SavingPaymentsModelDto data)
        {
            return await PostAsync<SavingPaymentsModelDto, SavedPaymentsDocumentResultDto>(
                    $"/MoneyPayments/SavePaymentDocuments?firmId={firmId}&userId={userId}",
                    data, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public async Task<FundPaymentsDto> GetFundPaymentsAsync(int firmId, int userId,
            GetFundPaymentsRequestDto request)
        {
            return await PostAsync<GetFundPaymentsRequestDto, FundPaymentsDto>(
                    $"/MoneyPayments/GetFundPayments?firmId={firmId}&userId={userId}",
                    request, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public async Task<SavedPaymentsDocumentResultDto> SaveFundPaymentAsync(int firmId, int userId,
            SavingFundPaymentDto request)
        {
            return await PostAsync<SavingFundPaymentDto, SavedPaymentsDocumentResultDto>(
                    $"/MoneyPayments/SaveFundPayments?firmId={firmId}&userId={userId}",
                    request, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public async Task<PaymentsForReportDto> GetFundPaymentsForReportAsync(int firmId, int userId,
            GetFundPaymentsForReportRequestDto request)
        {
            return await PostAsync<GetFundPaymentsForReportRequestDto, PaymentsForReportDto>(
                    $"/MoneyPayments/GetFundPaymentsForReport?firmId={firmId}&userId={userId}",
                    request, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public async Task<RefundFromBudgetPaymentsDto> GetRefundFromBudgetPayments(int firmId, int userId, GetFundPaymentsForReportRequestDto request)
        {
            return await PostAsync<GetFundPaymentsForReportRequestDto, RefundFromBudgetPaymentsDto>(
                    $"/MoneyPayments/GetRefundFromBudgetPayments?firmId={firmId}&userId={userId}",
                    request, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public async Task<PaymentByWorkerDto> GetWorkerPaymentsAsync(int firmId, int userId, int workerId,
            DateTime startDate, DateTime endDate)
        {
            return await GetAsync<PaymentByWorkerDto>(
                    "/MoneyPayments/GetWorkerPayments",
                    new { firmId, userId, workerId, startDate, endDate }, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public async Task<bool> HasDependenciesByWorkerAsync(int firmId, int userId, int workerId)
        {
            return await GetAsync<bool>(
                    "/MoneyPayments/HasDependenciesByWorker",
                    new { firmId, userId, workerId }, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public async Task<AccountingBalanceForPaymentDto> GetBalancesAsync(int firmId, int userId, DateTime date)
        {
            return await GetAsync<AccountingBalanceForPaymentDto>(
                    "/MoneyPayments/GetBalances",
                    new { firmId, userId, date }, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public async Task<PaymentsForReportDto> GetFundMoneyForReportAsync(int firmId, int userId,
            GetFundBizPaymentsRequestDto request)
        {
            return await PostAsync<GetFundBizPaymentsRequestDto, PaymentsForReportDto>(
                    $"/PaymentsForReport/GetBizPayments?firmId={firmId}&userId={userId}", request, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public Task<DateTime?> GetBalancesDateAsync(int firmId, int userId)
        {
            return GetAsync<DateTime?>("/MoneyPayments/GetBalancesDate", new { firmId, userId }, null, defaultSetting);
        }

        protected override HttpQuerySetting DefaultHttpQuerySetting()
        {
            return defaultSetting;
        }
    }
}