using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Money;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Money.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Client
{
    [InjectAsSingleton]
    public class MoneyApiClient : BaseCoreApiClient, IMoneyApiClient
    {
        private readonly ISettingRepository settingRepository;
        private const string prefix = "/api/v1";

        public MoneyApiClient(
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

        public async Task<List<OperationResponseDto>> GetOperationsAsync(
            int firmId,
            int userId,
            TaxationSystemType taxationSystemType,
            PaymentSources? source = null)
        {
            var request = new RegistryQueryDto
            {
                OperationSource = source
            };

            if (taxationSystemType != TaxationSystemType.Default)
            {
                request.TaxationSystemType = (int)taxationSystemType;
            }

            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var uri = $"{prefix}/Registry";
            var response = await GetAsync<ApiDataResult<List<OperationResponseDto>>>(
                uri,
                request,
                queryHeaders: tokenHeaders).ConfigureAwait(false);

            return response.data;
        }

        public async Task<List<OperationResponseDto>> GetOperationsAsync(int firmId, int userId, RegistryQueryDto request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var uri = $"{prefix}/Registry";
            var response = await PostAsync<RegistryQueryDto, ApiDataResult<List<OperationResponseDto>>>(
                uri,
                request,
                queryHeaders: tokenHeaders).ConfigureAwait(false);

            return response.data;
        }

        public async Task<List<RentPaymentPeriodDto>> GetByPaymentBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> ids)
        {
            if (ids?.Any() != true)
            {
                return new List<RentPaymentPeriodDto>();
            }

            var path = "PaymentOrders/Outgoing/RentPayment/Periods/GetByPaymentBaseIds";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<IReadOnlyCollection<long>, ApiDataResult<List<RentPaymentPeriodDto>>>(
                        $"{prefix}/{path}",
                        ids,
                        queryHeaders: tokenHeaders).ConfigureAwait(false);

            return response.data;
        }
    }
}
