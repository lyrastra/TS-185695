using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AgentsV2.Client.Dto.Partner;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AgentsV2.Client.PartnerReward
{
    [InjectAsSingleton]
    public class PartnerRewardSettingsApiClient : BaseApiClient, IPartnerRewardSettingsApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PartnerRewardSettingsApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AgentsApiUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<PartnerRewardSettingsDto> GetByPartnerIdAsync(int partnerId)
        {
            return GetAsync<PartnerRewardSettingsDto>("/PartnerRewardSettings/GetByPartnerId", new { partnerId });
        }

        public Task SaveAsync(PartnerRewardSettingsDto dto)
        {
            return PostAsync<PartnerRewardSettingsDto>("/PartnerRewardSettings/SavePartner", dto);
        }
    }
}