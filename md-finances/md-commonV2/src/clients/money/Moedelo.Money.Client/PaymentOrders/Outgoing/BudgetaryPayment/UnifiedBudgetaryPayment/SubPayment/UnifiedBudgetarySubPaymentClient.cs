using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Money.Dto;
using Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment;

namespace Moedelo.Money.Client.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment.SubPayment
{
    [InjectAsSingleton]
    public class UnifiedBudgetarySubPaymentClient: BaseCoreApiClient, IUnifiedBudgetarySubPaymentClient
    {
        private readonly ISettingRepository settingRepository;
        private const string Prefix = "/private/api/v1/PaymentOrders/Outgoing/UnifiedBudgetaryPayment/SubPayments";
        
        public UnifiedBudgetarySubPaymentClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("MoneyApiEndpoint").Value;
        }


        public async Task<UnifiedBudgetarySubPaymentResponseDto[]> GetByParentIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() == false)
            {
                return Array.Empty<UnifiedBudgetarySubPaymentResponseDto>();
            }
            
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<IReadOnlyCollection<long>, ApiDataResult<UnifiedBudgetarySubPaymentResponseDto[]>>(
                $"{Prefix}/GetByParentIds", documentBaseIds, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task<UnifiedBudgetarySubPaymentResponseDto[]> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds, CancellationToken ct)
        {
            if (documentBaseIds?.Any() == false)
            {
                return Array.Empty<UnifiedBudgetarySubPaymentResponseDto>();
            }

            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<IReadOnlyCollection<long>, ApiDataResult<UnifiedBudgetarySubPaymentResponseDto[]>>(
                $"{Prefix}/GetByBaseIds", documentBaseIds, queryHeaders: tokenHeaders, cancellationToken: ct).ConfigureAwait(false);
            return response.data;
        }
    }
}