using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.ElectronicReportsV2.Dto.StatusInfo;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ElectronicReportsV2.Client.StatusInfo
{
    [InjectAsSingleton]
    public class ElectronicReportsStatusInfoClient : BaseApiClient, IElectronicReportsStatusInfoClient
    {
        private readonly SettingValue apiEndpoint;
        
        public ElectronicReportsStatusInfoClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<ElectronicReportsStatusInfoForFirmResponseDto> GetForFirmByInnAsync(string inn)
        {
            return GetAsync<ElectronicReportsStatusInfoForFirmResponseDto>("/ElectronicReportsStatusInfo/GetForFirmByInn", new {inn});
        }

        public Task<List<ElectronicReportsStatusInfoForFirmResponseDto>> GetForFirmsByIdsAsync(
            IReadOnlyCollection<int> firmIds)
        {
            if (!firmIds.Any())
            {
                return Task.FromResult(new List<ElectronicReportsStatusInfoForFirmResponseDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, List<ElectronicReportsStatusInfoForFirmResponseDto>>(
                "/ElectronicReportsStatusInfo/GetForFirmsByIds", firmIds);
        }
    }
}