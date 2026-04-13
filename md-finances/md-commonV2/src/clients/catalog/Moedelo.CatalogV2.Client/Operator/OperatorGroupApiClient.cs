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
    public class OperatorGroupApiClient : BaseApiClient, IOperatorGroupApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public OperatorGroupApiClient(
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
            return apiEndPoint.Value + "/OperatorGroup/V2";
        }

        public Task<OperatorGroupDto> GetByIdAsync(int id)
        {
            return GetAsync<OperatorGroupDto>("/GetById", new {id});
        }

        public Task<List<OperatorGroupDto>> GetAllAsync()
        {
            return GetAsync<List<OperatorGroupDto>>("/GetAll");
        }
    }
}