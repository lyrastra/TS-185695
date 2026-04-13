using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Eds.Dto.EdsExpireInformation;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Eds.Client.EdsExpireInformation
{
    [InjectAsSingleton(typeof(IEdsExpireApiClient))]
    public class EdsExpireApiClient : BaseCoreApiClient, IEdsExpireApiClient
    {
        private const string dateFormat = "yyyy-MM-dd";

        private readonly SettingValue apiEndpoint;

        public EdsExpireApiClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator,
            IResponseParser responseParser, ISettingRepository settingRepository, IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator,
            responseParser, settingRepository, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("EdsPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndpoint.Value}/api/v1";
        }
        
        public Task<IReadOnlyList<ExpirationOnDateDto>> GetExpireInformationAsync(DateTime onDate)
        {
            return GetAsync<IReadOnlyList<ExpirationOnDateDto>>($"/expiration/ondate", new { onDate = onDate.ToString(dateFormat) });
        }

        public Task<ExpirationOnDateDto> GetExpireInformationByFirmIdAsync(int firmId, DateTime onDate)
        {
            return GetAsync<ExpirationOnDateDto>($"/expiration/ondate/byFirm", new { firmId, onDate = onDate.ToString(dateFormat) });
        }
    }
}