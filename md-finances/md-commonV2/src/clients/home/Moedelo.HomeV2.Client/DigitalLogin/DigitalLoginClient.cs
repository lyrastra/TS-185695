using Moedelo.HomeV2.Dto.DigitalLogin;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.DigitalLogin
{
    [InjectAsSingleton]
    public class DigitalLoginClient : BaseApiClient, IDigitalLoginClient
    {
        private readonly SettingValue apiEndPoint;

        protected DigitalLoginClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("HomePrivateApiEndpoint");
        }

        public async Task<List<DigitalLoginEmailDto>> GetEmailsByDigitalLoginAsync(string digitalLogin)
        {
            var response = await GetAsync<ListWrapper<DigitalLoginEmailDto>>("/GetEmailsByDigitalLogin", new { digitalLogin }).ConfigureAwait(false);
            return response.Items;
        }

        protected override string GetApiEndpoint()
        {
            return  apiEndPoint.Value + "/Rest/DigitalLogin";
        }
    }
}
