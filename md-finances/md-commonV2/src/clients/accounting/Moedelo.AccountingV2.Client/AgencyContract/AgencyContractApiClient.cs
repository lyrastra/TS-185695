using Moedelo.AccountingV2.Client.AgencyContract.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.AgencyContract
{
    [InjectAsSingleton]
    public class AgencyContractApiClient : BaseApiClient, IAgencyContractApiClient
    {
        private readonly SettingValue apiEndPoint;

        public AgencyContractApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<long> CreateOrUpdateAsync(int firmId, int userId, AgencyContractDto dto)
        {
            return PostAsync<AgencyContractDto, long>($"/AgencyContractApi/CreateOrUpdateAsync?firmId={firmId}&userId={userId}", dto);
        }

        public Task<AgencyContractDto> GetOrderByBaseIdAsync(int firmId, int userId, long id)
        {
            return PostAsync<AgencyContractDto>($"/AgencyContractApi/GetOrderByBaseIdAsync?firmId={firmId}&userId={userId}&Id={id}");
        }

        public Task DeleteAsync(int firmId, int userId, long id)
        {
            return PostAsync($"/AgencyContractApi/DeleteAsync?firmId={firmId}&userId={userId}&Id={id}");
        }
    }
}
