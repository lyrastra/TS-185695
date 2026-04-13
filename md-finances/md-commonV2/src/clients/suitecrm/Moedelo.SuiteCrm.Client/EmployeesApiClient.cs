using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SuiteCrm.Dto;
using Moedelo.SuiteCrm.Dto.Bpm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.SuiteCrm.Client
{
    [InjectAsSingleton]
    public class EmployeesApiClient : BaseApiClient, IEmployeesApiClient
    {
        private readonly SettingValue apiEndPoint;

        public EmployeesApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) :
            base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("OutsourceEmployeeApiEndpoint");
        }

        public async Task<EmployeeDto[]> GetByIdsAsync(int accountId, IReadOnlyCollection<int> ids)
        {
            var uri = $"/v1/GetByIds?accountId={accountId}";
            var response = await PostAsync<IReadOnlyCollection<int>, DataResponse<EmployeeDto[]>>(uri, ids)
                .ConfigureAwait(false);

            return response?.Data;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}
