using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.KontragentsV2.Dto;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    [InjectAsSingleton]
    public class KontragentImportClient : BaseApiClient, IKontragentImportClient
    {
        private readonly SettingValue apiEndpoint;

        public KontragentImportClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("KontragentsPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/ImportV2";
        }

        public Task<int> CreateAsync(int firmId, int userId, KontragentImportDto dto)
        {
            return PostAsync<KontragentImportDto, int>($"/Create?firmId={firmId}&userId={userId}", dto);
        }

        public Task DeleteAsync(int firmId, int userId, Guid guid)
        {
            return GetAsync("/Delete", new { firmId, userId, guid });
        }

        public Task<List<KontragentImportDto>> GetAsync(int firmId, int userId, Guid guid)
        {
            return base.GetAsync<List<KontragentImportDto>>("/Get", new { firmId, userId, guid });
        }

        public Task UpdateAsync(int firmId, int userId, KontragentImportDto dto)
        {
            return PostAsync($"/Update?firmId={firmId}&userId={userId}", dto);
        }
    }
}
