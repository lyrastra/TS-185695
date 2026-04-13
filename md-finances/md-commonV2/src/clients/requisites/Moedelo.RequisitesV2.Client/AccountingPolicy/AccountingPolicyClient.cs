using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto;
using Moedelo.RequisitesV2.Dto.AccountingPolicy;
using System.Threading.Tasks;

namespace Moedelo.RequisitesV2.Client.AccountingPolicy
{
    [InjectAsSingleton(typeof(IAccountingPolicyClient))]
    public class AccountingPolicyClient : BaseApiClient, IAccountingPolicyClient
    {
        private readonly SettingValue apiEndPoint;

        public AccountingPolicyClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) :
            base(
                httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager
                )
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<AccountingPolicyDto> GetOrCreateAsync(int firmId, int userId, int year)
        {
            return PostAsync<AccountingPolicyDto>($"/AccountingPolicy/GetOrCreate?firmId={firmId}&userId={userId}&year={year}");
        }

        public Task SaveAsync(int firmId, int userId, AccountingPolicyDto dto)
        {
            return PostAsync($"/AccountingPolicy/Save?firmId={firmId}&userId={userId}", dto);
        }

        public Task<AccountingPolicyChangeSettingsDto> CanTransferUsnOsnoAsync(int firmId, int userId)
        {
            return GetAsync<AccountingPolicyChangeSettingsDto>(
                $"/AccountingPolicy/CanTransferUsnOsno?firmId={firmId}&userId={userId}");
        }

        public Task<AccountingPolicyChangeResultDto> TransferUsnOsnoAsync(int firmId, int userId, TaxationSystemType taxType)
        {
            return PostAsync<AccountingPolicyChangeResultDto>(
                $"/AccountingPolicy/TransferUsnOsno?firmId={firmId}&userId={userId}&taxType={taxType}");
        }
    }
}