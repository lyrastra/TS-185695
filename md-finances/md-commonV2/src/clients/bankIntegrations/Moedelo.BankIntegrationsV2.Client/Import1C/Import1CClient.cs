using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.Import1C;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrationsV2.Client.Import1C
{
    [InjectAsSingleton]
    public class Import1CClient : BaseApiClient, IImport1CClient
    {
        private const string ControllerName = "/Import1C";
        private readonly SettingValue apiEndPoint;

        public Import1CClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public Task<Import1CFileResponseDto> Import1CFileAsync(Import1CFileRequestDto requestDto)
        {
            return PostAsync<Import1CFileRequestDto, Import1CFileResponseDto>("/Import1CFile", requestDto);
        }
    }
}