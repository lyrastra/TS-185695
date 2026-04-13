using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Money.Dto;
using Moedelo.Money.Dto.PaymentOrders;
using Moedelo.Money.Dto.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton]
    public class PaymentToNaturalPersonsClient : BaseCoreApiClient, IPaymentToNaturalPersonsClient
    {
        private readonly ISettingRepository settingRepository;
        private const string prefix = "/api/v1";

        public PaymentToNaturalPersonsClient(
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

        public async Task<PaymentOrderSaveResponseDto> CreateAsync(int firmId, int userId, PaymentToNaturalPersonsSaveDto dto)
        {
            var path = "PaymentOrders/Outgoing/PaymentToNaturalPersons";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<PaymentToNaturalPersonsSaveDto, ApiDataResult<PaymentOrderSaveResponseDto>>(
                $"{prefix}/{path}", dto, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task<PaymentToNaturalPersonResponseDto> GetAsync(int firmId, int userId, long documentBaseId)
        {
            var path = $"PaymentOrders/Outgoing/PaymentToNaturalPersons/{documentBaseId}";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<PaymentToNaturalPersonResponseDto>>(
                $"{prefix}/{path}", null, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }
        
        public async Task<PaymentToNaturalPersonResponseDto[]> GetByBaseIdsIdAsync(
            int firmId, int userId,
            IReadOnlyCollection<long> documentBaseIds,
            CancellationToken cancellationToken)
        {
            if (!documentBaseIds.Any())
            {
                return Array.Empty<PaymentToNaturalPersonResponseDto>();
            }

            const string url = "/private/api/v1/PaymentOrders/Outgoing/PaymentToNaturalPersons/GetByBaseIds";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId, cancellationToken)
                .ConfigureAwait(false);

            var result = await PostAsync<IReadOnlyCollection<long>, ApiDataResult<PaymentToNaturalPersonResponseDto[]>>(
                    url, documentBaseIds, queryHeaders: tokenHeaders, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return result.data;
        }
    }
}
