using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.HistoricalLogsV2.Dto.BackofficeLog;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HistoricalLogsV2.Client.BackofficeLog
{
    [InjectAsSingleton]
    public class BackofficeLogReaderClient : BaseApiClient, IBackofficeLogReaderClient
    {
        private readonly SettingValue endpointSetting;

        public BackofficeLogReaderClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            endpointSetting = settingRepository.Get("HistoricalLogsApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return endpointSetting.Value;
        }

        public Task<List<BackofficeLogDto>> GetListAsync(BackofficeLogParameterDto dto)
        {
            return PostAsync<BackofficeLogParameterDto, List<BackofficeLogDto>>("/BackofficeLog/GetList", dto);
        }

        public Task<List<BackofficeLogDto>> GetListForObjectsAsync(BackofficeLogParameterByObjectIdsDto dto)
        {
            return PostAsync<BackofficeLogParameterByObjectIdsDto, List<BackofficeLogDto>>("/BackofficeLog/GetListForObjects", dto);
        }

        public Task<BackofficeLogActionDataDto> GetActionDataAsync(int id)
        {
            return GetAsync<BackofficeLogActionDataDto>("/BackofficeLog/GetActionData", new {id});
        }
    }
}