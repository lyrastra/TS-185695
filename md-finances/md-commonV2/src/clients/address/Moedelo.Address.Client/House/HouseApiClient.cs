using System.Threading.Tasks;
using Moedelo.Address.Dto.House;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Address.Client.House
{
    [InjectAsSingleton]
    public class HouseApiClient : BaseApiClient, IHouseApiClient
    {
        private readonly SettingValue apiEndPoint;

        public HouseApiClient(
            ISettingRepository settingRepository,
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, IResponseParser responseParser, 
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AddressApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/House";
        }
        public Task<HouseDto> GetByAoGuidAndNumber(HouseGetByRequestDto dto)
        {
            return GetAsync<HouseDto>("/GetByAoGuidAndNumber", dto);
        }

        
    }
}