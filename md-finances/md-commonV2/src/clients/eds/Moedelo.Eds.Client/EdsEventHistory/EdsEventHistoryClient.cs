using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Eds.Dto;
using Moedelo.Eds.Dto.EdsEventHistory;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Eds.Client.EdsEventHistory
{
    [InjectAsSingleton]
    public class EdsEventHistoryClient : BaseCoreApiClient, IEdsEventHistoryClient
    {
        private readonly SettingValue apiEndpoint;

        public EdsEventHistoryClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator,
            IResponseParser responseParser, ISettingRepository settingRepository, IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator,
            responseParser, settingRepository, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("EdsPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndpoint.Value}/api/v1";
        }

        public Task<bool> HasTransferFlagAsync(int firmId) =>
            GetAsync<bool>($"/EdsEventHistory/HasTransferFlag?firmId={firmId}");

        public Task<EdsTransferInfoDto> GetTransferInfoAsync(int firmId)
        {
            return GetAsync<EdsTransferInfoDto>($"/EdsEventHistory/GetTransferInfo?firmId={firmId}");
        }

        public Task<DataDto<string>> GetPreviousAbnGuidAsync(int firmId)
        {
            return GetAsync<DataDto<string>>($"/EdsEventHistory/GetPreviousAbnGuid?firmId={firmId}");
        }

        public Task<IReadOnlyList<FirmIdentificatorDto>> GetFirmIdentificatorsAfterTransferAsync(IReadOnlyList<string> guids)
        {
            return PostAsync<IReadOnlyList<string>, IReadOnlyList<FirmIdentificatorDto>>($"/EdsEventHistory/GetFirmIdentificatorsAfterTransfer", guids);
        }
    }
}