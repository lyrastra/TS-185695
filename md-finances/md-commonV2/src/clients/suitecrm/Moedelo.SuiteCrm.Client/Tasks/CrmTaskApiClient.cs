using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SuiteCrm.Dto.Tasks;

namespace Moedelo.SuiteCrm.Client.Tasks
{
    [InjectAsSingleton]
    public class CrmTaskApiClient : BaseApiClient, ICrmTaskApiClient
    {
        private readonly SettingValue apiEndPoint;

        public CrmTaskApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("SuiteCrmApiUrl");
        }

        public Task<bool> CreateSimpleTaskAsync(SimpleTaskDto taskDto)
        {
            return PostAsync<SimpleTaskDto, bool>("/Task/SimpleTask", taskDto);
        }

        public Task<bool> CreateTaskWithSelectedOwnerAsync(SimpleTaskWithFreeOwnerDto taskDto)
        {
            return PostAsync<SimpleTaskWithFreeOwnerDto, bool>("/Task/SimpleTaskWithOwner", taskDto);
        }

        public Task<bool> CreateSpendBonusesTask(SpendBonusesTaskDto taskDto)
        {
            return PostAsync<SpendBonusesTaskDto, bool>("/Task/SpendBonusesTask", taskDto);
        }

        public Task<bool> CreateClosingDocumentsTaskAsync(ClosingDocumentsTaskDto taskDto)
        {
            return PostAsync<ClosingDocumentsTaskDto, bool>("/Task/ClosingDocumentsTask", taskDto);
        }

        public Task<bool> CreateOutsourceRequestTaskAsync(OutsourceRequestTaskDto taskDto)
        {
            return PostAsync<OutsourceRequestTaskDto, bool>("/Task/OutsourceRequestTask", taskDto);
        }

        public Task<bool> CreateReactivationTaskAsync(CreateReactivationTaskDto taskDto)
        {
            return PostAsync<CreateReactivationTaskDto, bool>("/Task/ReactivationTask", taskDto);
        }

        public Task<bool> CreateCustomCommonEventTaskAsync(CustomCommonEventTaskDto taskDto)
        {
            return PostAsync<CustomCommonEventTaskDto, bool>("/Task/CustomCommonEventTask", taskDto);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}