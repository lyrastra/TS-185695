using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.BankIntegrationsNetCore;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.BankIntegrationsNetCore
{
    [InjectAsSingleton(typeof(IIntegratedUserClient))]
    public class IntegratedUserClient : BaseApiClient, IIntegratedUserClient
    {
        private const string IntegratedUserController = "/private/api/v1/IntegratedUser";

        public IntegratedUserClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<IntegratedUserClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("IntegrationApiNetCore"),
        logger)
        {
        }

        public async Task<IntegratedUserDto> GetLastIntegratedUserAsync(IntegrationPartners partner, CancellationToken cancellationToken = default)
        {
            var url = $"{IntegratedUserController}/LastIntegratedUser";
            var queryParams = new { partner };

            return await GetAsync<IntegratedUserDto>(url, queryParams, cancellationToken: cancellationToken);
        }

        public async Task<IntegratedUserDto> GetByFirmIdAsync(int firmId, IntegrationPartners partner, CancellationToken cancellationToken = default)
        {
            var url = $"{IntegratedUserController}/ByFirmId";
            var queryParams = new { firmId, partner };

            return await GetAsync<IntegratedUserDto>(url, queryParams, cancellationToken: cancellationToken);
        }

        public async Task<int> UpdateTokenDataAsync(UpdateTokenDataRequestDto dto, CancellationToken cancellationToken = default)
        {
            var response = await PostAsync<UpdateTokenDataRequestDto, ApiDataResult<int>>(
                uri: $"{IntegratedUserController}/UpdateTokenData",
                data: dto, cancellationToken: cancellationToken);

            return response.data;
        }

        public async Task<string> GetTokenDataAsync(IntegrationPartners partner, int firmId, CancellationToken cancellationToken = default)
        {
            var response = await GetAsync<ApiDataResult<string>>(
                uri: $"{IntegratedUserController}/GetTokenData?partner={(int)partner}&firmId={firmId}", cancellationToken: cancellationToken);

            return response.data;
        }

        public async Task<int> UpdateIntegrationDataAsync(UpdateIntegrationDataRequestDto dto, CancellationToken cancellationToken = default)
        {
            var response = await PostAsync<UpdateIntegrationDataRequestDto, ApiDataResult<int>>(
                uri: $"{IntegratedUserController}/UpdateIntegrationData",
                data: dto, cancellationToken: cancellationToken);

            return response.data;
        }

        public async Task<string> GetIntegrationDataAsync(IntegrationPartners partner, int firmId, CancellationToken cancellationToken = default)
        {
            var response = await GetAsync<ApiDataResult<string>>(
                uri: $"{IntegratedUserController}/GetIntegrationData?partner={(int)partner}&firmId={firmId}", cancellationToken: cancellationToken);

            return response.data;
        }

        public async Task<int> InsertAsync(IntegratedUserBaseDto dto, CancellationToken cancellationToken = default)
        {
            var response = await PostAsync<IntegratedUserBaseDto, ApiDataResult<int>>(
                uri: $"{IntegratedUserController}/Insert",
                data: dto, cancellationToken: cancellationToken);

            return response.data;
        }

        public async Task<IntegratedUserDto> GetIntegratedUserByIdInExternalSystemAsync(string externalSystemId, int integrationPartner, HttpQuerySetting setting = null, CancellationToken cancellationToken = default)
        {
            var result = await GetAsync<ApiDataResult<IntegratedUserDto>>($"/{IntegratedUserController}/GetIntegratedUserByIdInExternalSystem",
                new
                {
                    externalSystemId,
                    integrationPartner
                },
                setting: setting, cancellationToken: cancellationToken); ;

            return result.data;
        }

        public async Task<IReadOnlyList<IntegratedUserDto>> GetIntegratedUsersByExternalSystemIdAsync(string externalSystemId, int integrationPartner, CancellationToken cancellationToken = default)
        {
            var result = await GetAsync<ApiDataResult<IReadOnlyList<IntegratedUserDto>>>(
                $"/{IntegratedUserController}/GetIntegratedUsersByExternalSystemId",
                new
                {
                    externalSystemId,
                    integrationPartner
                }, 
                cancellationToken: cancellationToken);

            return result.data;
        }

        public async Task<IntegratedUsersPageDto> GetByPageAsync(int integratorId, int pageNumber, 
            int pageSize = 100, bool? isActive = null, HttpQuerySetting setting = null, CancellationToken cancellationToken = default)
        {
            var result = await GetAsync<ApiDataResult<IntegratedUsersPageDto>>(
                $"/{IntegratedUserController}/GetByPage", new { integratorId, pageNumber, pageSize, isActive }, setting: setting, cancellationToken: cancellationToken);
            return result.data;
        }

        public async Task<ExtentedIntegratedUserDto[]> GetForAutoImportAsync(IntegratedUserAutoImportRequestDto dto, HttpQuerySetting setting = null, CancellationToken cancellationToken = default)
        {
            var result = await GetAsync<ApiDataResult<ExtentedIntegratedUserDto[]>>(
                $"/{IntegratedUserController}/GetForAutoImport", dto, setting: setting, cancellationToken: cancellationToken);
            return result.data;
        }

        public async Task SaveAsync(IntegratedUserDto request, HttpQuerySetting setting = null, CancellationToken cancellationToken = default)
        {
            await PostAsync($"/{IntegratedUserController}/Save", request, setting: setting, cancellationToken: cancellationToken);
        }
        
        public async Task<IReadOnlyList<IntegrationPartnersInfoDto>> GetActiveIntegrationsPartnersInfoAsync(int firmId, CancellationToken cancellationToken = default)
        {
            var result =
                await GetAsync<ApiDataResult<IReadOnlyList<IntegrationPartnersInfoDto>>>(
                    $"{IntegratedUserController}/GetActiveIntegrationsPartnersInfo", new { firmId }, cancellationToken: cancellationToken);
            return result.data;
        }

        public async Task DisableIntegrationAsync(DisableIntegrationRequestDto request, HttpQuerySetting setting = null, CancellationToken cancellationToken = default)
        {
            await PostAsync($"/{IntegratedUserController}/DisableIntegration", request, setting: setting, cancellationToken: cancellationToken);
        }

        public async Task<IntegratedUserBaseDto> GetBaseByFirmIdAsync(int firmId, IntegrationPartners partner, CancellationToken cancellationToken = default)
        {
            var url = $"{IntegratedUserController}/BaseByFirmId";
            var queryParams = new { firmId, partner };

            return await GetAsync<IntegratedUserBaseDto>(url, queryParams, cancellationToken: cancellationToken);
        }

        public async Task<IReadOnlyList<IntegratedUserDto>> GetActiveIntegratedUsersByPartnerAsync(PartnerActiveIntegratedUsersRequestDto dto, CancellationToken cancellationToken = default)
        {
            var response = await PostAsync<PartnerActiveIntegratedUsersRequestDto, ApiDataResult<IReadOnlyList<IntegratedUserDto>>>(
                uri: $"{IntegratedUserController}/GetActiveIntegratedUsersByPartner",
                data: dto,
                cancellationToken: cancellationToken);

            return response.data;
        }
    }
}