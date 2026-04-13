using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.OfficeV2.Dto.Contragent;
using Moedelo.OfficeV2.Dto.File;

namespace Moedelo.OfficeV2.Client.Kontragents
{
    [InjectAsSingleton]
    public class KontragentApiClient : BaseApiClient, IKontragentApiClient
    {
        private const string ControllerUri = "/Rest/v1/contragent/";

        private readonly SettingValue apiEndpoint;

        public KontragentApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("OfficePrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + ControllerUri;
        }

        public async Task<FileResponseDto> GetExcerptAsync(string innOrOgrn, HttpQuerySetting setting = null)
        {
            var result = await GetAsync<DataDto<FileResponseDto>>($"{innOrOgrn}/excerpt", setting: setting)
                .ConfigureAwait(false);
            return result.Data;
        }

        public async Task<bool> IsExistAsync(string innOrOgrn)
        {
            var result = await GetAsync<DataDto<ContragentExistResponse>>($"{innOrOgrn}/exist").ConfigureAwait(false);
            return result.Data?.Exist ?? false;
        }
    }
}