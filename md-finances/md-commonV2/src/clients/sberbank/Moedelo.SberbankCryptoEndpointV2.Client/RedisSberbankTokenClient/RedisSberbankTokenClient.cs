using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SberbankCryptoEndpointV2.Dto.SberbankMoedeloToken;
using System.Threading.Tasks;

namespace Moedelo.SberbankCryptoEndpointV2.Client.RedisSberbankTokenClient
{
    [InjectAsSingleton]
    public class RedisSberbankTokenClient : BaseApiClient, IRedisSberbankTokenClient
    {
        private const string ControllerName = "/RedisSberbankToken/";
        private readonly SettingValue apiEndPoint;

        public RedisSberbankTokenClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("SberbankCryptoEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public Task<SberbankMoedeloTokenDto> GetTokenFromRedisByClientIdAsync(string clientId)
        {
            return GetAsync<SberbankMoedeloTokenDto>("GetTokenFromRedisByClientId", new { clientId = clientId });
        }

        public Task<SberbankMoedeloTokenSaveResponseDto> SaveTokenToRedisAsync(SberbankMoedeloTokenDto dto)
        {
            return PostAsync<SberbankMoedeloTokenDto, SberbankMoedeloTokenSaveResponseDto>("SaveTokenToRedis", dto);
        }
    }
}
