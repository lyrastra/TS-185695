using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Legacy;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.BankIntegrations.ApiClient.Legacy
{
#pragma warning disable CS0618
    [InjectAsSingleton(typeof(IIntegratedUserApiClient))]
    public class IntegratedUserApiClient : BaseApiClient, IIntegratedUserApiClient
#pragma warning restore CS0618
    {
        private const string ControllerName = "IntegratedUser";
        public IntegratedUserApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<IntegratedUserApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("IntegrationApi"),
                logger)
        {
        }

        public async Task<IntegratedUserDto> GetIntegratedUserByIdInExternalSystemAsync(string externalSystemId, int integrationPartner, HttpQuerySetting setting = null)
        {
            var result = await GetAsync<DataResponseWrapper<IntegratedUserDto>>($"/{ControllerName}/GetIntegratedUserByIdInExternalSystem",
                new
                {
                    externalSystemId,
                    integrationPartner
                }, 
                setting: setting);

            return result.Data;
        }

        public async Task<IntegratedUserDto> GetIntegratedUserByFirmId(int firmId, int integrationPartner, HttpQuerySetting setting = null)
        {
            var result = await GetAsync<DataResponseWrapper<IntegratedUserDto>>($"/{ControllerName}/GetIntegratedUser", new { firmId, integrationPartner }, setting: setting);
            return result.Data;
        }

        public async Task<IntegratedUsersPageDto> GetByPageAsync(int integrationPartner, int page, HttpQuerySetting setting = null)
        {
            var result = await GetAsync<DataResponseWrapper<IntegratedUsersPageDto>>($"/{ControllerName}/GetByPage", new { page, integrationPartner }, setting: setting);
            return result.Data;
        }

        public async Task SaveAsync(IntegratedUserDto request, HttpQuerySetting setting = null)
        {
            await PostAsync($"/{ControllerName}/Save", request, setting: setting);
        }

        public async Task SaveFromSso(SaveFromSsoDto request, HttpQuerySetting setting = null)
        {
            await PostAsync($"/{ControllerName}/SaveFromSso", request, setting: setting);
        }
        
        public async Task<List<IntegrationPartners>> GetActiveIntegrationsForFirmAsync(int firmId)
        {
            var result = await GetAsync<DataResponseWrapper<List<IntegrationPartners>>>(
                    $"/{ControllerName}/GetActiveIntegrationsForFirm", new { firmId });
            return result?.Data;
        }

        public async Task<IntegratedUserDto> GetLastIntegratedUserAsync(int integrationPartner, HttpQuerySetting setting = null)
        {
            var result = await GetAsync<DataResponseWrapper<IntegratedUserDto>>($"/{ControllerName}/GetLastIntegratedUser",
                new { integrationPartner }, setting: setting);
            return result.Data;
        }
    }
}