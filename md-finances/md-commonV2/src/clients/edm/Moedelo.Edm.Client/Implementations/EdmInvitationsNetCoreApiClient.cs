using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Edm.Client.Contracts;
using Moedelo.Edm.Dto.Invitations;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Edm.Client.Implementations
{
    [InjectAsSingleton(typeof(IEdmInvitationsNetCoreApiClient))]
    public class EdmInvitationsNetCoreApiClient : BaseCoreApiClient, IEdmInvitationsNetCoreApiClient
    {
        private readonly SettingValue endPointSetting;

        public EdmInvitationsNetCoreApiClient(
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
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            endPointSetting = settingRepository.Get("EdmInvitationsPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return endPointSetting.Value;
        }

        public async Task<GetEnabledResponseDto> GetEnabledKontragentByIdsAsync(int firmId, int userId,
            GetEnabledRequestDto request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            return (await PostAsync<GetEnabledRequestDto, GetEnabledResponseDto>(
                    $"/api/v1/Status/GetEnabledKontragentByIds?firmId={firmId}&userId={userId}", request, queryHeaders: tokenHeaders)
                .ConfigureAwait(false));
        }
    }
}
