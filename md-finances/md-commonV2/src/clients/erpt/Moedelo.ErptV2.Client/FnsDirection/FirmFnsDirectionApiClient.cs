using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.ErptV2.Dto.FnsDirection;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.FnsDirection
{
    [InjectAsSingleton]
    public class FirmFnsDirectionApiClient : BaseApiClient, IFirmFnsDirectionApiClient
    {
        private readonly SettingValue apiEndpoint;
        
        public FirmFnsDirectionApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(
            httpRequestExecutor,
            uriCreator,
            responseParser,
            auditTracer,
            auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }
        
        public Task<List<FirmFnsDirectionCodesDto>> GetCodesAsync(IReadOnlyCollection<int> firmIds)
        {
            return PostAsync<IReadOnlyCollection<int>, List<FirmFnsDirectionCodesDto>>("/FirmFnsDirections/GetCodes", firmIds);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}