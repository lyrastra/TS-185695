using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Finances.Client.Money.Dtos;
using Moedelo.Finances.Dto.Kontragents;
using Moedelo.Finances.Dto.Money;
using Moedelo.Finances.Dto.Money.Operations;
using Moedelo.Finances.Dto.Money.Operations.Requests;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Finances.Client.Money
{
    [InjectAsSingleton]
    public class MoneyOperationsClient : BaseApiClient, IMoneyOperationsClient
    {
        private readonly SettingValue apiEndpoint;

        public MoneyOperationsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("FinancesPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        /// <inheritdoc />
        public Task<List<KontragentTurnoverDto>> TopByOperationsWithKontragents(int firmId, int userId, int count, DateTime startDate, DateTime endDate)
        {
            return GetAsync<List<KontragentTurnoverDto>>("/Money/TopByOperationsWithKontragents", new { firmId, userId, count, startDate, endDate});
        }

        public Task<long> GetDocumentBaseId(int firmId, int userId, long id)
        {
            return GetAsync<long>("/Money/GetDocumentBaseId", new { firmId, userId, id });
        }

        public Task<List<PaymentOrderStatusDto>> GetStatusesByBaseIds(int firmId, int userId,
            IReadOnlyCollection<long> documentsBaseIds, CancellationToken cancellationToken)
        {
            if (documentsBaseIds?.Any() != true)
            {
                return Task.FromResult(new List<PaymentOrderStatusDto>());
            }

            var uri = $"/Money/GetStatusesByBaseIds?firmId={firmId}&userId={userId}";

            return PostAsync<IReadOnlyCollection<long>, List<PaymentOrderStatusDto>>(uri, documentsBaseIds, cancellationToken: cancellationToken);
        }

        public Task<bool> HasOperationsBySettlementAccountAsync(int firmId, int userId, int settlementAccountId)
        {
            return GetAsync<bool>("/Money/HasOperationsBySettlementAccount", new { firmId, userId, settlementAccountId });
        }

        public async Task<IEnumerable<BudgetaryPaymentForReportDto>> GetBudgetaryPaymentsAsync(int firmId, int userId, GetBudgetaryAccPaymentsRequestDto request)
        {
            return await PostAsync<GetBudgetaryAccPaymentsRequestDto, IEnumerable<BudgetaryPaymentForReportDto>>(
                    $"/Money/Operations/PaymentsForReport/GetBudgetaryPayments?firmId={firmId}&userId={userId}", request)
                .ConfigureAwait(false);
        }
        
        public async Task<IEnumerable<BudgetaryPaymentForReportDto>> GetBudgetaryPaymentsV2Async(int firmId, int userId, BudgetaryAccPaymentsRequestDto request)
        {
            return await PostAsync<BudgetaryAccPaymentsRequestDto, IEnumerable<BudgetaryPaymentForReportDto>>(
                    $"/Money/Operations/PaymentsForReport/V2/GetBudgetaryPayments?firmId={firmId}&userId={userId}", request)
                .ConfigureAwait(false);
        }

        public async Task<List<UsnBudgetaryPrepaymentDto>> GetUsnBudgetaryPrepaymentsAsync(int firmId, int userId, int year, bool needCashOperations)
        {
            return await GetAsync<List<UsnBudgetaryPrepaymentDto>>(
                    $"/Money/Operations/PaymentsForReport/GetUsnBudgetaryPrepayments?firmId={firmId}&userId={userId}&year={year}&needCashOperations={needCashOperations}")
                .ConfigureAwait(false);
        }

        public Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds.Count == 0)
            {
                return Task.CompletedTask;
            }
            return DeleteAsync($"/Money?firmId={firmId}&userId={userId}", documentBaseIds);
        }
        
        public async Task<List<MoneySourceTypeAndBaseIdDto>> GetTypesByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentsBaseIds)
        {
            if (documentsBaseIds?.Any() != true)
            {
                return new List<MoneySourceTypeAndBaseIdDto>();
            }

            return await PostAsync<IReadOnlyCollection<long>, List<MoneySourceTypeAndBaseIdDto>>($"/Money/GetTypesByBaseIds?firmId={firmId}&userId={userId}", documentsBaseIds).ConfigureAwait(false);
        }

        public Task<List<PaymentOrderOperationsStateDto>> GetOperationsStateByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentsBaseIds)
        {
            if (documentsBaseIds?.Any() != true)
            {
                return Task.FromResult(new List<PaymentOrderOperationsStateDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, List<PaymentOrderOperationsStateDto>>($"/Money/GetOperationsStateByBaseIds?firmId={firmId}&userId={userId}", documentsBaseIds);
        }

        public Task<List<MoneyOperationDto>> GetOperationsByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentsBaseIds)
        {
            if (documentsBaseIds?.Any() != true)
            {
                return Task.FromResult(new List<MoneyOperationDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, List<MoneyOperationDto>>($"/Money/GetByBaseIds?firmId={firmId}&userId={userId}", documentsBaseIds);
        }
    }
}