using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.BankIntegrationsV2.Dto.IntegratedUser;
using Moedelo.BankIntegrationsV2.Dto.Integrations;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Moedelo.BankIntegrationsV2.Client.IntegratedUser
{
#pragma warning disable CS0618 // Type or member is obsolete
    [InjectAsSingleton(typeof(IIntegratedUserClient))]
    public sealed class IntegratedUserClient : BaseApiClient, IIntegratedUserClient
#pragma warning restore CS0618 // Type or member is obsolete
    {
        private const string ControllerName = "/IntegratedUser/";
        private readonly SettingValue apiEndPoint;

        public IntegratedUserClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public Task SaveForTesterAsync(SaveForTesterDto dto)
        {
            return PostAsync("SaveForTester", dto);
        }

        public Task SaveFromSsoAsync(SaveFromSsoDto dto)
        {
            return PostAsync("SaveFromSso", dto);
        }

        public async Task<IntegratedUserDto> GetIntegratedUserAsync(int firmId, IntegrationPartners integrationPartner)
        {
            var queryParams = new { firmId, integrationPartner };
            var responseWrapper = await GetAsync<DataResponseWrapper<IntegratedUserDto>>("GetIntegratedUser", queryParams).ConfigureAwait(false);
            return responseWrapper.Data;
        }

        public Task<IntegratedUsersPageDto> GetPartnerIntegratedUsersAsync(
            IntegrationPartners integrationPartner,
            int pageNumber,
            uint pageSize,
            bool? isActive,
            HttpQuerySetting setting)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), pageNumber, "Не может быть меньше 1");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "Не может быть меньше 1");
            }

            return GetAsync<IntegratedUsersPageDto>(
                $"GetByPage",
                new { pageNumber, pageSize, integratorId = (int)integrationPartner, isActive },
                setting: setting);
        }

        public Task DisableIntegrationAsync(DisableIntegrationRequestDto dto)
        {
            return PostAsync("DisableIntegration", dto);
        }

        public async Task<List<IntegrationPartners>> GetActiveIntegrationsForFirmAsync(int firmId)
        {
            return (await GetAsync<GetActiveIntegrationsForFirmResponse>("GetActiveIntegrationsForFirm", new { firmId }).ConfigureAwait(false)).Data;
        }

        public Task<List<FirmIntegrationPartnerDto>> GetActiveIntegrationsForFirmsAsync(IReadOnlyCollection<int> firmIds)
        {
            return PostAsync<IReadOnlyCollection<int>, List<FirmIntegrationPartnerDto>>("GetActiveIntegrationsForFirms", firmIds);
        }

        public async Task<List<IntegrationPartnersInfoDto>> GetActiveIntegrationsPartnersInfoAsync(int firmId)
        {
            var responseWrapper = await GetAsync<DataResponseWrapper<List<IntegrationPartnersInfoDto>>>("GetActiveIntegrationsPartnersInfo", new { firmId }).ConfigureAwait(false);
            return responseWrapper.Data;
        }

        public Task<int> GetAcceptancePriceListAsync(int firmId, IntegrationPartners partner)
        {
            return GetAsync<int>($"GetAcceptancePriceList?firmId={firmId}&partner={partner}");
        }

        public Task SetAcceptancePriceListAsync(NextAcceptancePriceDto nextAcceptancePriceDto)
        {
            return PostAsync($"SetAcceptancePriceList", nextAcceptancePriceDto);
        }

        public async Task<List<IntegrationPartners>> GetActiveIntegrationsForSendPaymentAsync(int firmId)
        {
            var response =
                await GetAsync<DataResponseWrapper<List<IntegrationPartners>>>("GetActiveIntegrationsForSendPayment",
                    new { firmId }).ConfigureAwait(false);

            return response.Data;
        }

        public async Task<IntegratedUserDto> GetIntegratedUserByIdInExternalSystem(string externalSystemId, IntegrationPartners integrationPartner)
        {
            var result = await GetAsync<DataResponseWrapper<IntegratedUserDto>>("GetIntegratedUserByIdInExternalSystem", new { externalSystemId, integrationPartner }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<bool> GetActiveIntegrationsLastSuccessfulSettlementRequestAsync(DateTime start, DateTime end, int partner, string settlementNumber, int firmId)
        {
            var result = await GetAsync<DataResponseWrapper<bool>>($"GetActiveIntegrationsLastSuccessfulSettlementRequest",
                new
                {
                    beginDate = start,
                    endDate = end,
                    partner,
                    settlementNumber,
                    firmId
                }, setting: new HttpQuerySetting(TimeSpan.FromSeconds(60))).ConfigureAwait(false);

            return result.Data;
        }

        public async Task<IntegrationLimitsInfoDto> GetIntegrationLimitsAsync(int firmId)
        {
            var result = await GetAsync<DataResponseWrapper<IntegrationLimitsInfoDto>>("GetIntegrationLimit", new { firmId }).ConfigureAwait(false);
            return result.Data;
        }

        public Task<bool> GetIsPatentAsync(int firmId, IntegrationPartners partner)
        {
            return GetAsync<bool>($"GetIsPatent?firmId={firmId}&partner={partner}");
        }
        
        public async Task<bool> ResetAcceptancePriceListAsync(int integratorId, int firmId)
        {
            var result = await PostAsync<bool>($"/ResetAcceptancePriceList?integratorId={integratorId}&firmId={firmId}")
                .ConfigureAwait(false);

            return result;
        }

        public Task CreateAsync(IntegratedUserCreateRequestDto requestDto)
        {
            return SaveAsync(
                new IntegratedUserDto
                {
                    FirmId = requestDto.FirmId,
                    IsActive = requestDto.IsActive,
                    IntegrationPartner = requestDto.IntegrationPartner,
                    IntegrationData = requestDto.IntegrationData,
                    ExternalClientId = requestDto.ExternalClientId,
                    CreateDate = DateTime.Now
                });
        }

        public Task UpdateAsync(IntegratedUserUpdateRequestDto requestDto)
        {
            return SaveAsync(
                new IntegratedUserDto
                {
                    Id = requestDto.Id,
                    FirmId = requestDto.FirmId,
                    IsActive = requestDto.IsActive,
                    IntegrationPartner = requestDto.IntegrationPartner,
                    IntegrationData = requestDto.IntegrationData,
                    ExternalClientId = requestDto.ExternalClientId
                });
        }

        public Task DeleteAsync(int integratedUserId)
        {
            return DeleteAsync($"Delete?integratedUserId={integratedUserId}");
        }
        
        // исторически есть такой метод, но IntegratedUserDto перенасыщена полями,
        // которые на самом деле не будут сохраняться, поэтому не хочется выставлять
        // такой класс Dto в публиный метод на создание/изменение 
        private Task SaveAsync(IntegratedUserDto request)
        {
            return PostAsync($"Save", request);
        }
    }
}
