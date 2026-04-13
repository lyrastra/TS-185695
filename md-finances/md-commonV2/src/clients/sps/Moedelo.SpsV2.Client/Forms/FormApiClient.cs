using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SpsV2.Dto.Forms;

namespace Moedelo.SpsV2.Client.Forms
{
    [InjectAsSingleton(typeof(IFormApiClient))]
    public class FormApiClient : BaseApiClient, IFormApiClient
    {
        private readonly SettingValue apiEndpoint;

        private const string controllerUri = "/Rest/Form";

        private const string getDataForWidgetUri = "/GetDataForWidget";

        public FormApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, 
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ProPrivateApiEndpoint");
        }

        public Task<FormWidgetResponseDto> GetDataForWidget(FormWidgetRequestDto request, int userId = 0)
        {
            return PostAsync<FormWidgetRequestDto, FormWidgetResponseDto>(getDataForWidgetUri, request);
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndpoint.Value}{controllerUri}";
        }
    }
}