using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SpsV2.Dto.Documents;

namespace Moedelo.SpsV2.Client.Documents
{
    [InjectAsSingleton(typeof(IDocumentApiClient))]
    public class DocumentApiClient : BaseApiClient, IDocumentApiClient
    {
        private readonly SettingValue apiEndPoint;

        private const string CONTROLLER_URI = "/Rest/Document";
        private const string GET_LANDING_QA_INFO_URI = "/GetLandingQaInfo";
        private const string GET_LANDING_FORM_INFO_URI = "/GetLandingFormInfo";
        private const string GET_CUSTOM_VALUES_URI = "/GetCustomValues";
        private const string GET_COMMON_PROPERTIES_URI = "/GetCommonProperties";
        private const string GET_XML_CONTENT_URI = "/GetXmlContent";
        private const string GET_FORM_HTML_URI = "/GetFormHtml";
        private const string GET_FORM_TEMPLATE_URI = "/GetFormTemplate";

        public DocumentApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("ProPrivateApiEndpoint");
        }

        public Task<LandingQaInfoDto> GetLandingQaInfo(QaInfoRequestDto request)
        {
            return PostAsync<QaInfoRequestDto, LandingQaInfoDto>(GET_LANDING_QA_INFO_URI, request);
        }

        public Task<LandingFormInfoDto> GetLandingFormInfo(DocIdDto request)
        {
            return PostAsync<DocIdDto, LandingFormInfoDto>(GET_LANDING_FORM_INFO_URI, request);
        }

        public Task<List<CommonPropertyCustomValueDto>> GetCustomValues(CustomValuesRequestDto request)
        {
            return PostAsync<CustomValuesRequestDto, List<CommonPropertyCustomValueDto>>(GET_CUSTOM_VALUES_URI, request);
        }

        public Task<List<CommonPropertyDto>> GetCommonProperties()
        {
            return PostAsync<List<CommonPropertyDto>>(GET_COMMON_PROPERTIES_URI);
        }

        public Task<string> GetXmlContent(DocIdDto request)
        {
            return PostAsync<DocIdDto, string>(GET_XML_CONTENT_URI, request);
        }

        public Task<string> GetFormHtml(DocIdDto request)
        {
            return PostAsync<DocIdDto, string>(GET_FORM_HTML_URI, request);
        }

        public Task<FormTemplateDto> GetFormTemplate(DocIdDto request)
        {
            return PostAsync<DocIdDto, FormTemplateDto>(GET_FORM_TEMPLATE_URI, request);
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndPoint.Value}{CONTROLLER_URI}";
        }
    }
}
