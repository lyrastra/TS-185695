using System;
using System.Collections.Generic;
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
    [InjectAsSingleton(typeof(IAccPaymentsApiClient))]
    internal sealed class AccPaymentsApiClient : BaseApiClient, IAccPaymentsApiClient
    {
        private readonly HttpQuerySetting defaultSetting = new HttpQuerySetting(TimeSpan.FromMinutes(5));

        private readonly SettingValue apiEndPoint;
        
        public AccPaymentsApiClient(
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
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<WorkerPaymentWithNdflDto> GetWorkerPaymentWithNdflAsync(int firmId, int userId, DateTime startDate, DateTime endDate, DateTime chargedStartDate)
        {
            return await GetAsync<WorkerPaymentWithNdflDto>(
                    "/SalaryPayments/GetWorkerPaymentWithNdfl",
                    new {firmId, userId, startDate, endDate, chargedStartDate}, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public async Task<WorkerPaymentWithNdflDto> GetNdflPaymentsAsync(int firmId, int userId, DateTime startDate, DateTime endDate, DateTime chargedStartDate)
        {
            return await GetAsync<WorkerPaymentWithNdflDto>(
                    "/SalaryPayments/GetNdflPayments",
                    new {firmId, userId, startDate, endDate, chargedStartDate}, null, defaultSetting)
                .ConfigureAwait(false);
        }
        
        public async Task<WorkerRefundFromBudgetPaymentWithNdflDto> GetNdflRefundFromBudgetPaymentsAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return await GetAsync<WorkerRefundFromBudgetPaymentWithNdflDto>(
                    "/SalaryPayments/GetNdflRefundFromBudgetPayments",
                    new {firmId, userId, startDate, endDate}, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public async Task<SalaryPaymentsDto> GetSalaryPaymentsAsync(int firmId, int userId, GetSalaryPaymentsRequestDto request)
        {
            return await PostAsync<GetSalaryPaymentsRequestDto, SalaryPaymentsDto>(
                    $"/SalaryPayments/GetSalaryPayments?firmId={firmId}&userId={userId}",
                    request, null, defaultSetting)
                .ConfigureAwait(false);
        }
        
        public async Task<DocumentsNumbersDto> GetPaymentDocumentLastNumbersAsync(int firmId, int userId, GetPaymentDocumentLastNumbersRequestDto request)
        {
            return await PostAsync<GetPaymentDocumentLastNumbersRequestDto, DocumentsNumbersDto>(
                    $"/SalaryPayments/GetPaymentDocumentLastNumbers?firmId={firmId}&userId={userId}", 
                    request, null, defaultSetting)
                .ConfigureAwait(false);
        }
        
        public async Task<SavedPaymentsDocumentResultDto> SavePaymentDocumentsAsync(int firmId, int userId, SavingPaymentsModelDto data)
        {
            return await PostAsync<SavingPaymentsModelDto, SavedPaymentsDocumentResultDto>(
                    $"/SalaryPayments/SavePaymentDocuments?firmId={firmId}&userId={userId}", 
                    data, null, defaultSetting)
                .ConfigureAwait(false);
        }
        
        public async Task<FundPaymentsDto> GetFundPaymentsAsync(int firmId, int userId, GetFundPaymentsRequestDto request)
        {
            return await PostAsync<GetFundPaymentsRequestDto, FundPaymentsDto>(
                    $"/SalaryPayments/GetFundPayments?firmId={firmId}&userId={userId}", 
                    request, null, defaultSetting)
                .ConfigureAwait(false);
        }
        
        public async Task<SavedPaymentsDocumentResultDto> SaveFundPaymentAsync(int firmId, int userId, SavingFundPaymentDto request)
        {
            return await PostAsync<SavingFundPaymentDto, SavedPaymentsDocumentResultDto>(
                    $"/SalaryPayments/SaveFundPayments?firmId={firmId}&userId={userId}", 
                    request, null, defaultSetting)
                .ConfigureAwait(false);
        }
        
        public async Task<PaymentsForReportDto> GetFundPaymentsForReportAsync(int firmId, int userId, GetFundPaymentsForReportRequestDto request)
        {
            return await PostAsync<GetFundPaymentsForReportRequestDto, PaymentsForReportDto>(
                    $"/SalaryPayments/GetFundPaymentsForReport?firmId={firmId}&userId={userId}", 
                    request, null, defaultSetting)
                .ConfigureAwait(false);
        }
        
        public async Task<RefundFromBudgetPaymentsDto> GetRefundFromBudgetPaymentsAsync(int firmId, int userId, GetFundPaymentsForReportRequestDto request)
        {
            return await PostAsync<GetFundPaymentsForReportRequestDto, RefundFromBudgetPaymentsDto>(
                    $"/SalaryPayments/GetRefundFromBudgetPayments?firmId={firmId}&userId={userId}", 
                    request, null, defaultSetting)
                .ConfigureAwait(false);
        }
        
        public async Task<PaymentByWorkerDto> GetWorkerPaymentsAsync(int firmId, int userId, int workerId, DateTime startDate, DateTime endDate)
        {
            return await GetAsync<PaymentByWorkerDto>(
                    "/SalaryPayments/GetWorkerPayments",
                    new {firmId, userId, workerId, startDate, endDate}, null, defaultSetting)
                .ConfigureAwait(false);
        }
        
        public async Task<PaymentByWorkerDto> GetWorkersPaymentsAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return await GetAsync<PaymentByWorkerDto>(
                    "/SalaryPayments/GetWorkersPayments",
                    new {firmId, userId, startDate, endDate}, null, defaultSetting)
                .ConfigureAwait(false);
        }
        
        public async Task<bool> HasDependenciesByWorkerAsync(int firmId, int userId, int workerId)
        {
            return await GetAsync<bool>(
                    "/SalaryPayments/HasDependenciesByWorker",
                    new {firmId, userId, workerId}, null, defaultSetting)
                .ConfigureAwait(false);
        }
        
        public async Task<AccountingBalanceForPaymentDto> GetBalancesAsync(int firmId, int userId, DateTime date, DateTime? fromDate = null)
        {
            return await GetAsync<AccountingBalanceForPaymentDto>(
                    "/SalaryPayments/GetBalances",
                    new {firmId, userId, date, fromDate}, null, defaultSetting)
                .ConfigureAwait(false);
        }
        
        public async Task<List<DeductionPaymentDto>> GetDeductionPaymentsAsync(int firmId, int userId, DateTime endDate)
        {
            return await GetAsync<List<DeductionPaymentDto>>(
                    "/SalaryPayments/GetDeductionPayments",
                    new {firmId, userId, endDate}, null, defaultSetting)
                .ConfigureAwait(false);
        }

        public async Task<PaymentsForReportDto> GetFundMoneyForReportAsync(int firmId, int userId, GetFundAccPaymentsRequestDto request)
        {
            return await PostAsync<GetFundAccPaymentsRequestDto, PaymentsForReportDto>(
                    $"/PaymentsForReport/GetAccPayments?firmId={firmId}&userId={userId}", request, null, defaultSetting)
                .ConfigureAwait(false);
        }
        
        public async Task<SavedWorkerPaymentDto> GetWorkerPaymentByDocumentBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            return await GetAsync<SavedWorkerPaymentDto>(
                    "/SalaryPayments/GetWorkerPaymentByDocumentBaseId",
                    new {firmId, userId, documentBaseId}, null, defaultSetting)
                .ConfigureAwait(false);
        }

        protected override HttpQuerySetting DefaultHttpQuerySetting()
        {
            return defaultSetting;
        }
    }
}