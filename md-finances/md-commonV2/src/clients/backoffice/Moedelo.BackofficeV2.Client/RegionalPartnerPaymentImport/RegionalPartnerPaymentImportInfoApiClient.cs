using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.RegionalPartnerPaymentImport;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BackofficeV2.Client.RegionalPartnerPaymentImport
{
    [InjectAsSingleton(typeof(IRegionalPartnerPaymentImportInfoApiClient))]
    public class RegionalPartnerPaymentImportInfoApiClient : BaseApiClient, IRegionalPartnerPaymentImportInfoApiClient
    {
        private readonly SettingValue apiEndPoint;

        public RegionalPartnerPaymentImportInfoApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("BackOfficePrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<IReadOnlyCollection<RegionalPartnerPaymentImportInfoDto>> GetByIds(IReadOnlyCollection<int> ids)
        {
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<RegionalPartnerPaymentImportInfoDto>>(
                "/Rest/RegionalPartnerPaymentImportInfo/GetByIds", ids);
        }
    }
}
