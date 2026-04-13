using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CashV2.Client.Contracts;
using Moedelo.CashV2.Dto.YandexKassa;
using Moedelo.CashV2.Dto.YandexKassa.Integration;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CashV2.Client.Implementations
{
    [InjectAsSingleton]
    public class YandexKassaApiClient : BaseApiClient, IYandexKassaApiClient
    {
        private readonly SettingValue apiEndPoint;

        public YandexKassaApiClient(
            IHttpRequestExecutor requestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(requestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("CashPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<bool> UpdatePreferedTaxationSystem(int firmId, long shopId, TaxationSystemType taxationSystem)
        {
            return PostAsync<bool>($"/YandexKassa/UpdatePreferedTaxationSystem?firmId={firmId}&shopId={shopId}&preferedTaxationSystem={(int)taxationSystem}");
        }

        public Task<GetAndSaveBankExtractsDto> UpdateIntegrationsInfo(int firmId, int userId, IntegrationsRequestDto integrations)
        {
            return PostAsync<IntegrationsRequestDto, GetAndSaveBankExtractsDto>($"/YandexKassaV2/UpdateIntegrationsInfo?firmId={firmId}&userId={userId}", 
                integrations, queryHeaders: null, setting: new HttpQuerySetting(TimeSpan.FromMinutes(15)));
        }

        public Task<YandexKassaIntegrationResponseDto> SendShopAccessGrantEmailAsync(int firmId, int userId, YandexKassaIntegrationRequestDto yandexKassaIntegrationRequest)
        {
            return PostAsync<YandexKassaIntegrationRequestDto, YandexKassaIntegrationResponseDto>($"/YandexKassa/SendShopAccessGrantEmail?firmId={firmId}&userId={userId}", yandexKassaIntegrationRequest);
        }

        public Task<bool> TurnYandexKassaShopIntegrationAsync(int firmId, int userId, YandexKassaShopIntegrationTurnClientData yandexKassaShop)
        {
            return PostAsync<YandexKassaShopIntegrationTurnClientData, bool>($"/YandexKassa/TurnYandexKassaShopIntegration?firmId={firmId}&userId={userId}", yandexKassaShop);
        }

        public Task<YandexKassaAddShopResultDto> AddYandexKassaIntegratedShopAsync(int firmId, int userId, YandexKassaShopIntegrationTurnClientData yandexKassaShop)
        {
            var settings = new HttpQuerySetting(TimeSpan.FromMinutes(15));
            return PostAsync<YandexKassaShopIntegrationTurnClientData, YandexKassaAddShopResultDto>($"/YandexKassa/AddYandexKassaIntegratedShop?firmId={firmId}&userId={userId}", yandexKassaShop, null, settings);
        }

        public Task<bool> RemoveYandexKassaIntegratedShopAsync(int firmId, int userId, YandexKassaShopIntegrationTurnClientData yandexKassaShop)
        {
            return PostAsync<YandexKassaShopIntegrationTurnClientData, bool>($"/YandexKassa/RemoveYandexKassaIntegratedShop?firmId={firmId}&userId={userId}", yandexKassaShop);
        }
        
        public Task<List<YandexKassaShopClientData>> GetYandexKassaIntegratedShopListAsync(int firmId, int userId)
        {
            return GetAsync<List<YandexKassaShopClientData>>($"/YandexKassa/GetYandexKassaIntegratedShopList?firmId={firmId}&userId={userId}");
        }

        public Task<YandexKassaIntegrationStatusDto> GetYandexKassaIntegrationStatusAsync(int firmId, int userId)
        {
            return GetAsync<YandexKassaIntegrationStatusDto>($"/YandexKassa/GetYandexKassaIntegrationStatus?firmId={firmId}&userId={userId}");
        }

        public Task<YandexKassaRequestInfoDto> GetYandexKassaIntegrationRequestInfoAsync(int firmId, int userId)
        {
            return GetAsync<YandexKassaRequestInfoDto>($"/YandexKassa/GetYandexKassaIntegrationRequestInfo?firmId={firmId}&userId={userId}");
        }
    }
}