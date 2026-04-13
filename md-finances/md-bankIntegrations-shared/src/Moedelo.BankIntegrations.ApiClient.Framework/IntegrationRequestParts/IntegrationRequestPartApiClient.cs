using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationRequestParts;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrations.ApiClient.Framework.IntegrationRequestParts
{
    [InjectAsSingleton(typeof(IIntegrationRequestPartApiClient))]
    internal sealed class IntegrationRequestPartApiClient : BaseCoreApiClient, IIntegrationRequestPartApiClient
    {
        private readonly SettingValue endpoint;
        
        public IntegrationRequestPartApiClient(
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
            this.endpoint = settingRepository.Get("IntegrationApiNetCore");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }

        public async Task<int> CreateNewAsync(int integrationRequestId, NewIntegrationRequestPartParamsDto paramsDto)
        {
            var url = $"/private/api/v1/integration/request/{integrationRequestId}/part";

            return await PostAsync<NewIntegrationRequestPartParamsDto, int>(
                    url,
                    paramsDto,
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task UpdateAsync(int integrationRequestId, int requestPartId, UpdateIntegrationRequestPartParamsDto paramsDto)
        {
            var url = $"/private/api/v1/integration/request/{integrationRequestId}/part/{requestPartId}";

            await PutAsync(
                    url,
                    paramsDto,
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }
        
        public async Task SetLogFileIdAsync(int integrationRequestId, int requestPartId, string logFileId)
        {
            var url = $"/private/api/v1/integration/request/{integrationRequestId}/part/{requestPartId}/LogFileId";

            await PutAsync(url, new ValueDto<string>(logFileId),
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task SetLogFileAsync(int integrationRequestId, int requestPartId, string logFile)
        {
            var url = $"/private/api/v1/integration/request/{integrationRequestId}/part/{requestPartId}/logFile";

            await PutAsync(url, new ValueDto<string>(logFile),
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IntegrationRequestPartDto> GetAsync(int integrationRequestId, int requestPartId)
        {
            var url = $"/private/api/v1/integration/request/{integrationRequestId}/part/{requestPartId}";

            return await GetAsync<IntegrationRequestPartDto>(url,
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<IntegrationRequestPartDto>> GetPartsOfRequestAsync(int integrationRequestId)
        {
            var url = $"/private/api/v1/integration/request/{integrationRequestId}/part";

            return await GetAsync<IReadOnlyCollection<IntegrationRequestPartDto>>(url,
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IntegrationRequestPartDto> GetByExternalRequestIdAsync(IntegrationPartners integrationPartner, string externalRequestId)
        {
            const string url = "/private/api/v1/integration/request/part/find-one";

            return await GetAsync<IntegrationRequestPartDto>(
                    url,
                    new { integrationPartner, externalRequestId },
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task SetStatusForAllPartsOfIntegrationRequestsAsync(
            IReadOnlyCollection<int> integrationRequestIds,
            IntegrationRequestPartStatusEnum newStatus)
        {
            const string url = "/private/api/v1/integration/request/part/set-multiple-parts-status";

            if (integrationRequestIds?.Any() != true)
            {
                return;
            }

            await PutAsync(
                    url,
                    new SetMultipleIntegrationRequestPartsStatusRequestDto
                    {
                        IntegrationRequestIds = integrationRequestIds,
                        NewStatus = newStatus
                    },
                    queryHeaders: await GetUnidentifiedTokenHeaders().ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}
