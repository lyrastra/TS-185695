using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.FirmActivityCategory;

namespace Moedelo.RequisitesV2.Client.FirmActivityCategory
{
    [InjectAsSingleton]
    public class FirmActivityCategoryClient : BaseApiClient, IFirmActivityCategoryClient
    {
        private readonly SettingValue apiEndPoint;
        
        public FirmActivityCategoryClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
                )
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<FirmActivityCategoryDto> GetMainAsync(int firmId, int userId)
        {
            const string uri = "/FirmActivityCategory/main";

            return GetAsync<FirmActivityCategoryDto>(uri, new { firmId, userId });
        }

        public Task SetMainAsync(int firmId, int userId, string code, CancellationToken cancellationToken)
        {
            var body = new ChangeFirmMainActivityCategoryRequestDto
            {
                Code = code
            };

            return PostAsync($"/FirmActivityCategory/main?firmId={firmId}&userId={userId}",
                body, cancellationToken: cancellationToken); 
        }

        public Task<List<FirmActivityCategoryDto>> GetMainAsync(IReadOnlyCollection<int> firmIds)
        {
            if (firmIds?.Any() != true)
            {
                return Task.FromResult(new List<FirmActivityCategoryDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, List<FirmActivityCategoryDto>>(
                "/FirmActivityCategory/GetMainByFirmIds", firmIds);
        }

        public async Task<FirmActivityCategoryDto> GetOutdatedAsync(int firmId, int userId)
        {
            var result = await GetAsync<FirmActivityCategoryResultDto>("/FirmActivityCategory/GetOutdated", new { firmId, userId }).ConfigureAwait(false);
            return result.Data;
        }
    }
}
