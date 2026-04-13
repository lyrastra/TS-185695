using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SpsV2.Dto.Rubrics;

namespace Moedelo.SpsV2.Client.Rubrics
{
    [InjectAsSingleton(typeof(IRubricApiClient))]
    public class RubricApiClient : BaseApiClient, IRubricApiClient
    {
        private readonly SettingValue apiEndpoint;

        private const string CONTROLLER_URI = "/Rest/Rubric";

        private const string GET_RUBRICS_INFO_URI = "/GetRubricsInfo";
        private const string GET_PARENT_RUBRICS_URI = "/GetParentRubrics";

        public RubricApiClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ProPrivateApiEndpoint");
        }

        public Task<RubricInfoDto> GetRubricsInfo(RubricInfoRequestDto request)
        {
            return PostAsync<RubricInfoRequestDto, RubricInfoDto>(GET_RUBRICS_INFO_URI, request);
        }

        public Task<List<RubricPathDto>> GetParentRubrics(RubricInfoRequestDto request)
        {
             return PostAsync<RubricInfoRequestDto, List<RubricPathDto>>(GET_PARENT_RUBRICS_URI, request);
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndpoint.Value}{CONTROLLER_URI}";
        }
    }
}
