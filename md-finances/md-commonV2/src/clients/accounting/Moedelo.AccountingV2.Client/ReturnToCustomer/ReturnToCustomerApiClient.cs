using Moedelo.AccountingV2.Client.ReturnToCustomer.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.ReturnToCustomer
{
    [InjectAsSingleton]
    public class ReturnToCustomerApiClient : BaseApiClient, IReturnToCustomerApiClient
    {
        private readonly SettingValue apiEndPoint;

        public ReturnToCustomerApiClient(
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

        public Task<long> CreateOrUpdateAsync(int firmId, int userId, ReturnToCustomerDto dto)
        {
            return PostAsync<ReturnToCustomerDto, long>($"/ReturnToCustomerApi/CreateOrUpdateAsync?firmId={firmId}&userId={userId}", dto);
        }

        public Task<ReturnToCustomerDto> GetOrderByBaseIdAsync(int firmId, int userId, long id)
        {
            return PostAsync<ReturnToCustomerDto>($"/ReturnToCustomerApi/GetOrderByBaseIdAsync?firmId={firmId}&userId={userId}&Id={id}");
        }

        public Task DeleteAsync(int firmId, int userId, long id)
        {
            return PostAsync($"/ReturnToCustomerApi/DeleteAsync?firmId={firmId}&userId={userId}&Id={id}");
        }
    }
}
