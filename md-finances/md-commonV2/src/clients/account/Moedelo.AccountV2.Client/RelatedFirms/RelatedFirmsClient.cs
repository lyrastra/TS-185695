using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.RelatedFirms;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.RelatedFirms
{
    [InjectAsSingleton]
    public class RelatedFirmsClient : BaseApiClient, IRelatedFirmsClient
    {
        private readonly SettingValue apiEndPoint;

        public RelatedFirmsClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            ISettingRepository settingRepository,
            IAuditScopeManager auditScopeManager) : base(httpRequestExecutor,
            uriCreator,
            responseParser,
            auditTracer,
            auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/RelatedFirms/";
        }

        public Task<RelatedFirmsInfoDto> GetAsync(int firmId)
        {
            return GetAsync<RelatedFirmsInfoDto>("", new {firmId});
        }

        public Task<List<MainFirmInfoDto>> GetMainFirmInfosAsync(IReadOnlyCollection<int> firmIds)
        {
            return PostAsync<IReadOnlyCollection<int>, List<MainFirmInfoDto>>("GetMainFirm", firmIds);
        }
    }
}