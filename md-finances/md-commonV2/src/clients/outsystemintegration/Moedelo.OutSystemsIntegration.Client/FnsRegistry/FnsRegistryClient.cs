using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.OutSystemsIntegrationV2.Dto.Registry;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.OutSystemsIntegrationV2.Client.FnsRegistry
{
    [InjectAsSingleton]
    public class FnsRegistryClient : BaseApiClient, IFnsRegistryClient
    {
        private readonly SettingValue apiEndpoint;
        
        public FnsRegistryClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("FnsRegistryApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/V2";
        }
        
        public Task<bool> CheckEgrulExclusionAsync(CheckEgrulExclusionInRegistryRequestDto request)
        {
            return PostAsync<CheckEgrulExclusionInRegistryRequestDto, bool>("/CheckEgrulExclusion", request);
        }

        public Task<bool> CheckFigureheadAsync(CheckFigureheadInRegistryRequestDto request)
        {
            return PostAsync<CheckFigureheadInRegistryRequestDto, bool>("/CheckFigurehead", request);
        }

        public Task<bool> CheckFictitiousAddressAsync(CheckFictitiousAddressRequestDto request)
        {
            return PostAsync<CheckFictitiousAddressRequestDto, bool>("/CheckFictitiousAddress", request);
        }

        public Task<CheckBlockedAccountsResponseDto> CheckBlockedAccountsAsync(CheckBlockedAccountsRequestDto request)
        {
            return PostAsync<CheckBlockedAccountsRequestDto, CheckBlockedAccountsResponseDto>("/CheckBlockedAccounts", 
                request, 
                setting: new HttpQuerySetting(TimeSpan.FromMinutes(2)));
        }
    }
}