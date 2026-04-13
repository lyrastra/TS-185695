using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountManagement.Dto.SharedFirms;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountManagement.Client.SharedFirms
{
    [InjectAsSingleton]
    public class SharedFirmsApiClient : BaseApiClient, ISharedFirmsApiClient
    {
        private readonly SettingValue apiEndPoint;

        public SharedFirmsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountManagementPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/SharedFirms";
        }

        public Task<List<FirmDto>> GetFirmsInAccountAsync(int userId, int firmId)
        {
            return GetAsync<List<FirmDto>>($"/GetFirmsInAccount?userId={userId}&firmId={firmId}");
        }

        public Task<CreateFirmInAccountResultDto> CreateFirmInAccountAsync(
            int firmId,
            int userId,
            CreateFirmInAccountRequestDto request)
        {
            return PostAsync<CreateFirmInAccountRequestDto, CreateFirmInAccountResultDto>(
                $"/Create?firmId={firmId}&userId={userId}",
                request);
        }
    }
}