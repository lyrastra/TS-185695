using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.KontragentsV2.Dto;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    [InjectAsSingleton]
    public class KontragentFromOfficeClient : BaseApiClient, IKontragentFromOfficeClient
    {
        private readonly SettingValue apiEndpoint;

        public KontragentFromOfficeClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("KontragentsPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        /// <inheritdoc />
        public Task<KontragentDto> GetAsync(int firmId, int userId, string inn)
        {
            if (string.IsNullOrEmpty(inn))
            {
                return Task.FromResult(default(KontragentDto));
            }
            
            return GetAsync<KontragentDto>(
                "/FromOffice",
                new {firmId, userId, inn});
        }
    }
}
