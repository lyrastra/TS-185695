using Moedelo.AccountingV2.Client.TransferRetailRevenue.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.TransferRetailRevenue
{
    [InjectAsSingleton]
    public class TransferRetailRevenueClient : BaseApiClient, ITransferRetailRevenueClient
    {
        private readonly SettingValue apiEndPoint;

        public TransferRetailRevenueClient(
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

        public Task<long> CreateAsync(int firmId, int userId, AccountingCashOrderDto dto)
        {
            return PostAsync<AccountingCashOrderDto, long>($"/Transfer/RetailRevenue/Create?firmId={firmId}&userId={userId}", dto);
        }
    }
}
