using Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegratedUser;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.BankIntegrations.Dto;
using System.Collections.Generic;
using System;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;

namespace Moedelo.BankIntegrations.ApiClient.Framework.IntegratedUser
{
    [InjectAsSingleton(typeof(IIntegratedUserApiClient))]
    public class IntegratedUserApiClient : BaseCoreApiClient, IIntegratedUserApiClient
    {
        private readonly SettingValue endpoint;
        private const string ControllerName = "/private/api/v1/IntegratedUser";

        public IntegratedUserApiClient(
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

        public async Task<int> InsertAsync(IntegratedUserBaseDto dto)
        {
            var response = await PostAsync<IntegratedUserBaseDto, ApiDataResult<int>>(
                uri: $"{ControllerName}/Insert",
                dto).ConfigureAwait(false);

            return response.data;
        }

        public async Task<IntegratedUserDto> GetLastIntegratedUserAsync(IntegrationPartners partner)
        {
            var url = $"{ControllerName}/LastIntegratedUser";
            var queryParams = new { partner };
            var queryHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            return await GetAsync<IntegratedUserDto>(url, queryParams, queryHeaders: queryHeaders).ConfigureAwait(false);
        }

        public async Task<string> GetIntegrationDataAsync(IntegrationPartners partner, int firmId)
        {
            var response = await GetAsync<ApiDataResult<string>>(
                uri: $"{ControllerName}/GetIntegrationData?partner={(int)partner}&firmId={firmId}").ConfigureAwait(false);

            return response.data;
        }

        public async Task<int> UpdateIntegrationDataAsync(UpdateIntegrationDataRequestDto dto)
        {
            var response = await PostAsync<UpdateIntegrationDataRequestDto, ApiDataResult<int>>(
                uri: $"{ControllerName}/UpdateIntegrationData",
                dto).ConfigureAwait(false);

            return response.data;
        }

        public async Task<IntegratedUserDto> GetIntegratedUserByIdInExternalSystemAsync(string externalSystemId, int integrationPartner)
        {
            var result = await GetAsync<ApiDataResult<IntegratedUserDto>>($"{ControllerName}/GetIntegratedUserByIdInExternalSystem",
                new
                {
                    externalSystemId,
                    integrationPartner
                }).ConfigureAwait(false);

            return result.data;
        }

        public async Task<IntegratedUserDto> GetByFirmIdAsync(int firmId, IntegrationPartners partner)
        {
            var url = $"{ControllerName}/ByFirmId";
            var queryParams = new { firmId, partner };

            return await GetAsync<IntegratedUserDto>(url, queryParams).ConfigureAwait(false);
        }

        public async Task DisableIntegrationAsync(DisableIntegrationRequestDto dto)
        {
            await PostAsync($"{ControllerName}/DisableIntegration", dto).ConfigureAwait(false);
        }

        public async Task SaveForTesterAsync(SaveForTesterDto dto)
        {
            await PostAsync($"{ControllerName}/SaveForTester", dto).ConfigureAwait(false);
        }

        public async Task<IntegratedUsersPageDto> GetPartnerIntegratedUsersAsync(
            IntegrationPartners integrationPartner,
            int pageNumber,
            uint pageSize,
            bool? isActive)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), pageNumber, "Не может быть меньше 1");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "Не может быть меньше 1");
            }

            var response = await GetAsync<ApiDataResult<IntegratedUsersPageDto>>(
                $"{ControllerName}/GetByPage",
                new { pageNumber, pageSize, integratorId = (int)integrationPartner, isActive }).ConfigureAwait(false);
            return response.data;
        }

        public async Task<List<FirmIntegrationPartnerDto>> GetActiveIntegrationsForFirmsAsync(IReadOnlyCollection<int> firmIds)
        {
            var response = await PostAsync<IReadOnlyCollection<int>, List<FirmIntegrationPartnerDto>>($"{ControllerName}/GetActiveIntegrationsForFirms", firmIds).ConfigureAwait(false);
            return response;
        }

        public async Task<List<IntegrationPartnersInfoDto>> GetActiveIntegrationsPartnersInfoAsync(int firmId)
        {
            var responseWrapper = await GetAsync<ApiDataResult<List<IntegrationPartnersInfoDto>>>($"{ControllerName}/GetActiveIntegrationsPartnersInfo", new { firmId }).ConfigureAwait(false);
            return responseWrapper.data;
        }

        public async Task<int> GetAcceptancePriceListAsync(int firmId, IntegrationPartners partner)
        {
            var response = await GetAsync<int>($"{ControllerName}/GetAcceptancePriceList?firmId={firmId}&partner={partner}").ConfigureAwait(false);
            return response;
        }

        public async Task SetAcceptancePriceListAsync(int firmId, int priceListId, IntegrationPartners partner)
        {
            await PostAsync($"{ControllerName}/SetAcceptancePriceList?firmId={firmId}&priceListId={priceListId}&partner={partner}").ConfigureAwait(false);
        }

        public async Task<List<IntegrationPartners>> GetActiveIntegrationsForSendPaymentAsync(int firmId)
        {
            var response =
                await GetAsync<ApiDataResult<List<IntegrationPartners>>>($"{ControllerName}/GetActiveIntegrationsForSendPayment",
                    new { firmId }).ConfigureAwait(false);

            return response.data;
        }

        public async Task<IntegratedUserDto> GetIntegratedUserByIdInExternalSystemAsync(string externalSystemId, IntegrationPartners integrationPartner)
        {
            var result = await GetAsync<ApiDataResult<IntegratedUserDto>>($"{ControllerName}/GetIntegratedUserByIdInExternalSystem", new { externalSystemId, integrationPartner }).ConfigureAwait(false);
            return result.data;
        }

        public async Task<bool> GetIsPatentAsync(int firmId, IntegrationPartners partner)
        {
            var response = await GetAsync<bool>($"{ControllerName}/GetIsPatent?firmId={firmId}&partner={partner}").ConfigureAwait(false);
            return response;
        }

        public async Task CreateOrUpdateAsync(IntegratedUserRequestDto requestDto)
        {
            await SaveAsync(
                new IntegratedUserDto
                {
                    FirmId = requestDto.FirmId,
                    IsActive = requestDto.IsActive,
                    IntegrationPartner = (int)requestDto.IntegrationPartner,
                    IntegrationData = requestDto.IntegrationData,
                    ExternalClientId = requestDto.ExternalClientId,
                    CreateDate = DateTime.Now,
                    EnabledDate = DateTime.Now
                }).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int integratedUserId)
        {
            await DeleteAsync($"{ControllerName}/Delete?integratedUserId={integratedUserId}").ConfigureAwait(false);
        }

        // исторически есть такой метод, но IntegratedUserDto перенасыщена полями,
        // которые на самом деле не будут сохраняться, поэтому не хочется выставлять
        // такой класс Dto в публиный метод на создание/изменение 
        public async Task SaveAsync(IntegratedUserDto request)
        {
            await PostAsync($"{ControllerName}/Save", request).ConfigureAwait(false);
        }

        public async Task SetAcceptanceLastErrorAsync(AcceptanceLastErrorDto request)
        {
            await PostAsync($"{ControllerName}/SetAcceptanceLastError", request).ConfigureAwait(false);
        }

        public async Task SetAcceptanceBlockAsync(AcceptanceBlockDto request)
        {
            await PostAsync($"{ControllerName}/SetAcceptanceBlock", request).ConfigureAwait(false);
        }

        public async Task<List<IntegrationIdentityDto>> IntegrationTurnGetIdentitiesAsync(IntegrationIdentityDto request)
        {
            var response = await PostAsync<IntegrationIdentityDto, ApiDataResult<List<IntegrationIdentityDto>>>($"{ControllerName}/IntegrationTurnGetIdentities", request).ConfigureAwait(false);
            return response.data;
        }

        public async Task<bool> ResetAcceptancePriceListAsync(int integratorId, int firmId)
        {
            var result = await PostAsync<bool>($"{ControllerName}/ResetAcceptancePriceList?integratorId={integratorId}&firmId={firmId}")
                .ConfigureAwait(false);

            return result;
        }
        
        public async Task SetAcceptanceLastError(AcceptanceLastErrorDto request)
        {
            await PostAsync($"{ControllerName}/SetAcceptanceLastError", request).ConfigureAwait(false);
        }

        public async Task SetAcceptanceBlock(AcceptanceBlockDto request)
        {
            await PostAsync($"{ControllerName}/SetAcceptanceBlock", request).ConfigureAwait(false);
        }
    }
}
