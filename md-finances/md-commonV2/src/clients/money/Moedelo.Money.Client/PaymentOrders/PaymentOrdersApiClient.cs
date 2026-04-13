using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Money.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.PaymentOrders
{
    [InjectAsSingleton]
    public class PaymentOrdersApiClient : BaseCoreApiClient, IPaymentOrdersApiClient
    {
        private readonly ISettingRepository settingRepository;
        private const string prefix = "/private/api/v1/PaymentOrders";

        public PaymentOrdersApiClient(
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

        public async Task<OperationTypeDto[]> GetOperationTypeByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() == false)
            {
                return Array.Empty<OperationTypeDto>();
            }
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<IReadOnlyCollection<long>, ApiDataResult<OperationTypeDto[]>>(
                $"{prefix}/GetTypeByBaseIds", documentBaseIds, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task ProvideAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() == false)
            {
                return;
            }
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await PostAsync($"{prefix}/Provide", documentBaseIds, queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        public async Task DeleteInvalidAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() == false)
            {
                return;
            }
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await DeleteByRequestAsync(
                $"{prefix}/Invalid",
                documentBaseIds,
                queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        public async Task<long> GetIdByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<long>>(
                $"{prefix}/{documentBaseId}/Id",
                queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task<long> GetDocumentBaseIdByIdAsync(int firmId, int userId, long id)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<long>>(
                $"{prefix}/{id}/DocumentBaseId",
                queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }
    }
}
