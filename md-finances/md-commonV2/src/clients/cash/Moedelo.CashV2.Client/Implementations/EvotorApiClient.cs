using Moedelo.CashV2.Client.Contracts;
using Moedelo.CashV2.Dto.Evotor;
using Moedelo.CashV2.Dto.Evotor.Partner;
using Moedelo.Common.Enums.Enums.Evotor.Sessions;
using Moedelo.CommonV2.Extensions.System;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.CashV2.Client.Implementations
{
    [InjectAsSingleton]
    public class EvotorApiClient : BaseApiClient, IEvotorApiClient
    {
        private readonly SettingValue apiEndPoint;

        public EvotorApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(
                  httpRequestExecutor,
                  uriCreator,
                  responseParser, auditTracer, auditScopeManager
                  )
        {
            apiEndPoint = settingRepository.Get("CashPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<IntegrationStatusDto> GetIntegrationStatusByFirmAsync(int firmId, int userId)
        {
            return GetAsync<IntegrationStatusDto>($"/EvotorMaintenance/GetIntegrationStatusByFirm?firmId={firmId}&userId={userId}");
        }

        public Task<bool> UpdateEvotorEntitiesForFirmAsync(int firmId, int userId, DateTime startDate, DateTime endDate, bool reloadRemovedDocuments = false)
        {
            return PostAsync<bool>($"/EvotorMaintenance/UpdateEvotorEntitiesForFirm?" +
                $"firmId={firmId}&userId={userId}&startDate={startDate.ToStrictString()}&endDate={endDate.ToStrictString()}&reloadRemovedDocuments={reloadRemovedDocuments}");
        }

        public Task DeactivateIntegrationForFirmAsync(int firmId, int userId)
        {
            return PostAsync<bool>($"/EvotorMaintenance/DeactivateIntegrationForFirm?firmId={firmId}&userId={userId}");
        }

        public Task<ListWithCountDto<EvotorSessionInfoDto>> GetSessionListWithCountAsync(
            int firmId,
            int userId,
            int offset = 0,
            int size = 50,
            SortType sortType = SortType.ByOpeningDateDesc)
        {
            return GetAsync<ListWithCountDto<EvotorSessionInfoDto>>($"/EvotorMaintenance/GetSessionsByFirm?" +
                $"firmId={firmId}&userId={userId}&offset={offset}&size={size}&sortType={sortType}");
        }

        public Task<bool> IsIntegrationActiveAsync(int firmId, int userId)
        {
            return GetAsync<bool>($"/EvotorMaintenance/IsIntegrationActive?firmId={firmId}&userId={userId}");
        }

        public Task<bool> HasEverBeenIntegratedAsync(int firmId, int userId, CancellationToken cancellationToken)
        {
            return GetAsync<bool>($"/EvotorMaintenance/HasEverBeenIntegrated?firmId={firmId}&userId={userId}", cancellationToken: cancellationToken);
        }

        public Task<IList<EvotorIntegrationDto>> GetIntegrationsAsync(bool onlyActive = false)
        {
            return GetAsync<IList<EvotorIntegrationDto>>($"/Evotor/GetIntegrations", new { onlyActive });
        }

        public Task ActualizeStatusAsync(EvotorIntegrationDto integration)
        {
            return PostAsync($"/Evotor/Actualize", integration);
        }

        public Task UpdateDataAsync(int firmId)
        {
            return PostAsync($"/Evotor/Update?firmId={firmId}");
        }
    }
}