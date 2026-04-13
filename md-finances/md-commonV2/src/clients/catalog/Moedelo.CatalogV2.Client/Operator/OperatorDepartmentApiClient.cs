using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Operator;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.Operator
{
    [InjectAsSingleton]
    public class OperatorDepartmentApiClient : BaseApiClient, IOperatorDepartmentApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public OperatorDepartmentApiClient(
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
            apiEndPoint = settingRepository.Get("CatalogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/OperatorDepartment/V2";
        }

        public Task<OperatorDepartmentDto> GetByIdAsync(int id)
        {
            return GetAsync<OperatorDepartmentDto>("/GetById", new {id});
        }

        public Task<List<OperatorDepartmentDto>> GetAllAsync()
        {
            return GetAsync<List<OperatorDepartmentDto>>("/GetAll", null);
        }
    }
}