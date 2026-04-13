using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Registration.Dto.RegistrationHistory;

namespace Moedelo.Registration.Client.RegistrationHistory
{
    [InjectAsSingleton]
    public class RegistrationHistoryApiClient : BaseApiClient, IRegistrationHistoryApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public RegistrationHistoryApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("RegistrationServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/RegistrationHistory";
        }

        public Task AddAsync(RegistrationHistoryDto registrationHistoryRequest)
        {
            return PostAsync("/Add", registrationHistoryRequest);
        }

        public Task UpdateUtmAsync(UtmDto utm)
        {
            return PostAsync("/UpdateUtm", utm);
        }

        public Task<List<RegistrationHistoryDto>> GetRegistrationHistoryByFirmIdAsync(int firmId)
        {
            return GetAsync<List<RegistrationHistoryDto>>("", new{firmId});
        }

        public Task<FirmRegistrationHistoryDto[]> GetRegistrationHistoryByFirmIdsAsync(IReadOnlyCollection<int> firmIds)
        {
            return PostAsync<IReadOnlyCollection<int>, FirmRegistrationHistoryDto[]>(
                "/GetRegistrationHistoryByFirmIds", firmIds);
        }

        public Task<FirmRegistrationHistoryDto[]> GetRegistrationHistoryByIdsAsync(IReadOnlyCollection<int> regIds)
        {
            return PostAsync<IReadOnlyCollection<int>, FirmRegistrationHistoryDto[]>($"/GetRegistrationHistoryByIds", regIds);
        }
    }
}