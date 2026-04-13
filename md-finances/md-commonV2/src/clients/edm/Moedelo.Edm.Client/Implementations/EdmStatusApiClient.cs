using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Edm.Client.Contracts;
using Moedelo.Edm.Dto.Status;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Edm.Client.Implementations
{
    [InjectAsSingleton]
    public class EdmStatusApiClient : BaseApiClient, IEdmStatusApiClient
    {
        private readonly SettingValue apiEndpoint;

        public EdmStatusApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("EdmPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/Rest/EdmStatus";
        }

        public Task<EdmStatusDto> GetEdmStatusAsync(int firmId, int userId)
        {
            return GetAsync<EdmStatusDto>("/Get", new { firmId, userId });
        }
    }
}
