using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.OutSystemsIntegrationV2.Dto;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.OutSystemsIntegrationV2.Client.CheckChangeEgrul
{
    [InjectAsSingleton]
    public class CheckChangeEgrulClient : BaseApiClient, ICheckChangeEgrulClient
    {
        private readonly SettingValue apiEndpoint;

        public CheckChangeEgrulClient(
            IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator,
            IResponseParser responseParser, 
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager, 
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("CheckChangeEgrulApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/V2";
        }

        public Task<List<string>> CheckAsync(CheckChangeEgrulRequestDto request)
        {
            var setting = new HttpQuerySetting(new TimeSpan(0, 0, 0, 10));
            return PostAsync<CheckChangeEgrulRequestDto, List<string>>("/CheckAsync", request, null, setting);
        }
    }
}